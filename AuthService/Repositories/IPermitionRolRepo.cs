using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IPermitionRolRepo
    {
        void CreateRelationPermitionRol(Rol rol, Permition permition);

        bool SaveChanges();
    }
}