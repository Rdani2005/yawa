using AccountService.Data;
using AccountService.Models;

namespace AccountService.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool ExternalUserExists(int ExternalId) =>
            _context.Users.Any(u => u.ExternalID == ExternalId);

        public IEnumerable<User> GetAllUsers() =>
            _context.Users.ToList();

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool UserExists(int id) =>
            _context.Users.Any(u => u.Id == id);
    }
}