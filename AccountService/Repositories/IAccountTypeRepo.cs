using AccountService.Models;

namespace AccountService.Repositories
{
    public interface IAccountTypeRepo
    {
        void CreateAccountType(AccountType type);

        IEnumerable<AccountType> GetAllTypes();

        AccountType GetTypeById(int id);
    }
}