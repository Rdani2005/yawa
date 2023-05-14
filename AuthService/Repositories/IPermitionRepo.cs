using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IPermitionRepo
    {
        bool SaveChanges();

        // Permitions
        IEnumerable<Permition> GetAllPermitions();
        IEnumerable<Permition> GetAllPermitionsByRol(int RoleId);
        Permition GetPermitionById(int Id);
        void CreatePermition(Permition permition);
    }
}