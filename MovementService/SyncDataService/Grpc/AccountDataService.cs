using AutoMapper;
using Grpc.Net.Client;
using MovementService.Models;
using AccountService;
namespace MovementService.SyncDataService.Grpc
{
    public class AccountDataService : IAccountDataService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AccountDataService(IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            Console.WriteLine($"--> Calling GRPC Service: {_config["GrpcAccounts"]}");
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress(_config["GrpcAccounts"], new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new GrpcAccount.GrpcAccountClient(channel);
            var request = new GetAllAccountsRequest();
            try
            {
                var reply = client.GetAllAccounts(request);
                return _mapper.Map<IEnumerable<Account>>(reply.Account);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"--> Couldnt call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}