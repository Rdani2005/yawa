using System.Text.Json;
using AccountService.Dtos;
using AccountService.Models;
using AccountService.Repositories;
using AutoMapper;

namespace AccountService.EventProcesing
{

    enum EventType
    {
        UserAdded,
        MovementDone,

        Undeterminated
    }

    public class EventProcessor : IEventProcessor
    {
        /// Resume
        ///     - Index of all available Event Types on The program.
        Dictionary<string, EventType> EventHandlers =
            new Dictionary<string, EventType>()
        {
            {"User_Published", EventType.UserAdded},
            {"Movement_Published", EventType.MovementDone},
            {"default", EventType.Undeterminated}
        };
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;


        public EventProcessor(
            IServiceScopeFactory scopeFactory,
            IMapper mapper
            )
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProccessEvent(string message)
        {
            var eventType = DeterminateEvent(message);
            switch (eventType)
            {
                case EventType.UserAdded:
                    AddUser(message);
                    break;
                case EventType.MovementDone:
                    PublishMovement(message);
                    break;
                default:
                    Console.WriteLine("--> Action not corresponding with this service.");
                    break;
            }

        }


        private EventType DeterminateEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            GenericDto eventType =
                JsonSerializer.Deserialize<GenericDto>(notificationMessage)!;

            return
                EventHandlers.ContainsKey(eventType!.Event) // Exists??
                    ? EventHandlers[eventType.Event] // yes
                    : EventHandlers["default"]; // No
        }

        private void AddUser(string userPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                IUserRepo repo = scope.ServiceProvider.GetRequiredService<IUserRepo>();
                UserPublishedDto userPublished =
                    JsonSerializer.Deserialize<UserPublishedDto>(userPublishedMessage)!;

                try
                {
                    var user = _mapper.Map<User>(userPublished);
                    if (!repo.ExternalUserExists(user.ExternalID))
                    {
                        repo.AddUser(user);
                    }

                    else
                    {
                        Console.WriteLine("--> User already exists.");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"--> Could Not add User to DB {ex.Message}");
                }
            }
        }

        private void PublishMovement(string movementPublished)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                IAccountRepo repo = scope.ServiceProvider.GetRequiredService<IAccountRepo>();
                MovementPublishedDto amountDto =
                    JsonSerializer.Deserialize<MovementPublishedDto>(movementPublished)!;

                try
                {
                    var account = repo.GetAccountById(amountDto.AccountId);
                    if (account != null)
                    {
                        account.ActualAmount += amountDto.MovementAmount;
                        repo.UpdateAccount(account);
                    }
                    else
                    {
                        Console.WriteLine("--> Account doesn't exists.");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"--> Could Not update the account to DB {ex.Message}");
                }
            }
        }



    }


}
