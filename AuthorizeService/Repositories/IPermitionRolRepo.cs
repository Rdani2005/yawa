using AuthorizeService.Models;

namespace AuthorizeService.Repositories
{
    public interface IPermitionRolRepo
    {
        void CreateRelationPermitionRol(Rol rol, Permition permition);

        bool SaveChanges();
    }
}