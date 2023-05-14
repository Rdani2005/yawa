using AccountService.Data;
using AccountService.Models;

namespace AccountService.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext _context;

        public AccountRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void DeleteAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }

        public Account GetAccountById(int id) =>
            _context.Accounts.FirstOrDefault(
                a => a.Id == id
            );

        public CoinType GetAccountCoin(int accountId)
        {
            return _context.Coins.FirstOrDefault(c => c.Id == accountId);
        }

        public IEnumerable<Account> GetAccountsForUser(int userId)
        {
            return _context.Accounts.Where(
                a => a.UserId == userId
            );
        }

        public AccountType GetAccountType(int accountId)
        {

            return _context.AccountTypes.FirstOrDefault(t => t.Id == accountId);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }

        public void UpdateAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            Account existingAccount = _context.Accounts.FirstOrDefault(a => a.Id == account.Id);
            if (existingAccount != null)
            {
                existingAccount.ActualAmount = account.ActualAmount;
            }
            _context.Accounts.Update(existingAccount);
            _context.SaveChanges();
        }
    }
}