using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IRolRepo
    {
        bool SaveChanges();

        // Rols
        IEnumerable<Rol> GetAllRols();
        IEnumerable<Rol> GetAllRolsByPermition(int permitionId);
        Rol GetRolById(int id);
        void CreateRol(Rol rol);
    }
}