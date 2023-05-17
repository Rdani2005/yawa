using MovementService.Models;

namespace MovementService.Repos
{
    public interface IAccountRepo
    {
        void CreateAccount(Account account);
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(int id);

        bool ExternalAccountExists(int ExternalId);
        bool AccountExists(int id);
    }
}