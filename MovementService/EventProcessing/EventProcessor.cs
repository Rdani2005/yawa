using System.Text.Json;
using AutoMapper;
using MovementService.Dtos;
using MovementService.Models;
using MovementService.Repos;

namespace MovementService.EventProcessing
{
    enum EventType
    {
        AccountAdded,
        Undeterminated
    }

    public class EventProcessor : IEventProcessor
    {

        Dictionary<string, EventType> EventHandlers =
            new Dictionary<string, EventType>()
            {
                {"Account_Published", EventType.AccountAdded},
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
                case EventType.AccountAdded:
                    Console.WriteLine("--> Adding new Account.");
                    AddAccount(message);
                    break;
                default:
                    Console.WriteLine("--> Message was not able to be here.");
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


        private void AddAccount(string accountPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                IAccountRepo repo = scope.ServiceProvider.GetRequiredService<IAccountRepo>();
                AccountPublishedDto accountPublished =
                    JsonSerializer.Deserialize<AccountPublishedDto>(accountPublishedMessage)!;

                try
                {
                    var account = _mapper.Map<Account>(accountPublished);
                    if (!repo.ExternalAccountExists(account.ExternalId))
                    {
                        repo.CreateAccount(account);
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

    }
}