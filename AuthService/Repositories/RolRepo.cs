using AuthService.Data;
using AuthService.Models;

namespace AuthService.Repositories
{
    public class RolRepo : IRolRepo
    {
        private readonly AppDbContext _context;

        public RolRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateRol(Rol rol)
        {
            _context.Rols.Add(rol);
        }

        public IEnumerable<Rol> GetAllRols() =>
            _context.Rols.ToList();

        public IEnumerable<Rol> GetAllRolsByPermition(int permitionId) =>
            _context.Rols.Where(
                    r => r.PermitionRols.Any(
                        pr => pr.PermitionId == permitionId
                    ));

        public Rol GetRolById(
            int id
        ) =>
            _context.Rols.FirstOrDefault(
                r => r.Id == id
            );

        public bool SaveChanges() =>
            _context.SaveChanges() >= 1;
    }
}