using AccountService.Models;

namespace AccountService.SyncDataServices.Grpc
{
    public interface IUserDataClient
    {
        IEnumerable<User> GetAllUsers();
    }
}