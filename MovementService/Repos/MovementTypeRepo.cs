using MovementService.Data;
using MovementService.Models;

namespace MovementService.Repos
{
    public class MovementTypeRepo : ITypeRepo
    {
        private readonly AppDbContext _context;

        public MovementTypeRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateType(MovementType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            _context.Types.Add(type);
            _context.SaveChanges();
        }

        public IEnumerable<MovementType> GetAllTypes() =>
            _context.Types.ToList();

        public MovementType GetMovementTypeById(int id) =>
            _context.Types.FirstOrDefault(t => t.Id == id);
    }
}