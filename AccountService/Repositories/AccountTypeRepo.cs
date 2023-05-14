using AccountService.Data;
using AccountService.Models;

namespace AccountService.Repositories
{
    public class AccountTypeRepo : IAccountTypeRepo
    {
        private readonly AppDbContext _context;

        public AccountTypeRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateAccountType(AccountType type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            _context.AccountTypes.Add(type);
            _context.SaveChanges();
        }

        public IEnumerable<AccountType> GetAllTypes()
        {
            return _context.AccountTypes.ToList();
        }

        public AccountType GetTypeById(int id)
        {
            return _context.AccountTypes.FirstOrDefault(t => t.Id == id);
        }
    }
}