using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IUserRepo
    {
        bool SaveChanges();

        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsersByRol(int roleId);

        User GetUserById(int id);
        Rol GetUserRol(int userId);
        IEnumerable<Permition> GetUserPermitions(int userId);

        User GetUserByIdentification(String identification);
        void CreateUser(User user);

        void UpdateUser(User user);

    }
}