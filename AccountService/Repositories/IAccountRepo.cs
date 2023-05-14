using AccountService.Models;

namespace AccountService.Repositories
{
    public interface IAccountRepo
    {
        void CreateAccount(Account account);

        Account GetAccountById(int id);
        IEnumerable<Account> GetAccountsForUser(int userId);

        IEnumerable<Account> GetAllAccounts();

        AccountType GetAccountType(int accountId);

        CoinType GetAccountCoin(int accountId);
        void UpdateAccount(Account account);

        void DeleteAccount(Account account);

    }
}