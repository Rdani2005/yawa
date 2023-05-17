using MovementService.Models;

namespace MovementService.SyncDataService.Grpc
{
    public interface IAccountDataService
    {
        IEnumerable<Account> GetAllAccounts();
    }
}