using AccountService.Models;

namespace AccountService.Repositories
{
    public interface IUserRepo
    {
        void AddUser(User user);

        bool ExternalUserExists(int ExternalId);
        bool UserExists(int id);

        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);

    }
}