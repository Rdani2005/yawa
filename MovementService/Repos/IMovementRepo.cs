using MovementService.Models;

namespace MovementService.Repos
{
    public interface IMovementRepo
    {
        void AddMovement(Movement movement, int accountId);
        Movement GetMovementById(int movementId, int accountId);
        IEnumerable<Movement> GetAllMovements(int accountId);
        MovementType GetMovementType(int movementTypeId);
    }
}