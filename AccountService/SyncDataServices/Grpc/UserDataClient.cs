using AccountService.Models;
using AuthService;
using AutoMapper;
using Grpc.Net.Client;

namespace AccountService.SyncDataServices.Grpc
{
    public class UserDataClient : IUserDataClient
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserDataClient(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public IEnumerable<User> GetAllUsers()
        {
            Console.WriteLine($"--> Calling GRPC Service: {_config["GrpcAuth"]}");
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress(_config["GrpcAuth"], new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new GrpcUser.GrpcUserClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllPUsers(request);
                return _mapper.Map<IEnumerable<User>>(reply.User);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"--> Couldnt call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}