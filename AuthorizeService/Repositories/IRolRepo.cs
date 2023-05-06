using AuthorizeService.Models;

namespace AuthorizeService.Repositories
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