using AuthorizeService.Data;
using AuthorizeService.Models;

namespace AuthorizeService.Repositories
{
    public class PermitionRolRepo : IPermitionRolRepo
    {
        private readonly AppDbContext _context;

        public PermitionRolRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateRelationPermitionRol(Rol rol, Permition permition)
        {

            PermitionRol relation = new PermitionRol()
            {
                Permition = permition,
                PermitionId = permition.Id,
                Rol = rol,
                RolId = rol.Id
            };
            _context.PermitionRols.Add(relation);
        }

        public bool SaveChanges() =>
            _context.SaveChanges() >= 1;
    }
}