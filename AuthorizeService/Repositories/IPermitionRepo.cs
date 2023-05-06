using AuthorizeService.Models;

namespace AuthorizeService.Repositories
{
    public interface IPermitionRepo
    {
        bool SaveChanges();

        // Permitions
        IEnumerable<Permition> GetAllPermitions();
        IEnumerable<Permition> GetAllPermitionsByRol(int RoleId);
        Permition GetPermitionById(int Id, int RolId);
        void CreatePermition(Permition permition);
    }
}