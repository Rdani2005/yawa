using MovementService.Data;
using MovementService.Models;

namespace MovementService.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext _context;

        public AccountRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool AccountExists(int id) =>
            _context.Accounts.Any(a => a.Id == id);

        public void CreateAccount(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public bool ExternalAccountExists(int ExternalId) =>
            _context.Accounts.Any(a => a.ExternalId == ExternalId);

        public Account GetAccountById(int id) =>
            _context.Accounts.FirstOrDefault(a => a.Id == id);

        public IEnumerable<Account> GetAllAccounts() =>
            _context.Accounts.ToList();
    }
}