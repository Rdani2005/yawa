using MovementService.Models;

namespace MovementService.Repos
{
    public interface ITypeRepo
    {
        void CreateType(MovementType type);
        MovementType GetMovementTypeById(int id);
        IEnumerable<MovementType> GetAllTypes();
    }
}