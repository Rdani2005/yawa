using System.Text;
using System.Text.Json;
using MovementService.Dtos;
using RabbitMQ.Client;

namespace MovementService.AsyncDataService
{
    public class MessageBus : IMessageBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _config;
        private readonly string triggerAction = "trigger";

        public MessageBus(IConfiguration config)
        {
            _config = config;
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: triggerAction, type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("--> Connected to Message Bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't connect to Message Bus {ex}");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Rabbit MQ Connection Shutdown");
        }


        public void PublishNewMovement(MovementPublishedDto movementPublished)
        {
            string message = JsonSerializer.Serialize(
                  movementPublished
              );
            if (!_connection.IsOpen)
            {
                Console.WriteLine("--> Rabbit MQ Connection close, NOT Sending message...");
                return;
            }

            Console.WriteLine("--> Rabbit MQ Connection Open, Sending message...");
            SendMessage(message);
        }

        private void SendMessage(string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: triggerAction,
                routingKey: "",
                basicProperties: null,
                body: body
                );
            Console.WriteLine($"--> Sended {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("--> Message Bus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}