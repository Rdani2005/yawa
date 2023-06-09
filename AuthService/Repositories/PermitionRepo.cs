using AuthService.Data;
using AuthService.Models;

namespace AuthService.Repositories
{
    public class PermitionRepo : IPermitionRepo
    {
        private readonly AppDbContext _context;

        public PermitionRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePermition(Permition permition)
        {
            if (permition == null)
                throw new ArgumentNullException(nameof(permition));
            _context.Permitions.Add(permition);

        }

        public IEnumerable<Permition> GetAllPermitions() =>
            _context.Permitions.ToList();

        public IEnumerable<Permition> GetAllPermitionsByRol(int RoleId)
        {
            return _context.Permitions.Where(
                p => p.PermitionRols.Any(
                    pr => pr.RolId == RoleId
                ));
        }



        public Permition GetPermitionById(
            int Id
        ) =>
            _context.Permitions.FirstOrDefault(
                p =>
                    p.Id == Id
            );
        public bool SaveChanges() =>
            _context.SaveChanges() >= 1;
    }
}