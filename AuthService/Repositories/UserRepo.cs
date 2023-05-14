using AuthService.Data;
using AuthService.Models;

namespace AuthService.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateUser(User user)
        {
            if (user == null) throw new ArgumentException(nameof(user));
            _context.Users.Add(user);
        }

        public IEnumerable<User> GetAllUsers() =>
            _context.Users.ToList();

        public IEnumerable<User> GetAllUsersByRol(int roleId) =>
            _context.Users.Where(u => u.UserRole.Id == roleId);

        public User GetUserById(int id) =>
            _context.Users.FirstOrDefault(u => u.Id == id);

        public User GetUserByIdentification(string identification) =>
            _context.Users.FirstOrDefault(u => u.Identification.Equals(identification));

        public IEnumerable<Permition> GetUserPermitions(int userId)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;
            var role = GetUserRol(user.Id);
            return _context.Permitions.Where(
                            p => p.PermitionRols.Any(
                                pr => pr.RolId == role.Id
                            ));
        }

        public Rol GetUserRol(int userId)
        {
            return _context.Rols.FirstOrDefault(r => r.Id == userId);
        }

        public bool SaveChanges() =>
            _context.SaveChanges() >= 1;

        public void UpdateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            User existingUser = GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.LastName = user.LastName;
                existingUser.Password = user.Password;
                existingUser.RoleId = user.RoleId;
                _context.Users.Update(existingUser);
                _context.SaveChanges();
            }
        }
    }
}