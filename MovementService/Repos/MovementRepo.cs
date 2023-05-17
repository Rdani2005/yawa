using MovementService.Data;
using MovementService.Models;

namespace MovementService.Repos
{
    public class MovementRepo : IMovementRepo
    {
        private readonly AppDbContext _context;

        public MovementRepo(AppDbContext context)
        {
            _context = context;
        }
        public void AddMovement(Movement movement, int accountId)
        {
            if (movement == null) throw new ArgumentNullException(nameof(movement));

            Account account = _context.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (movement.MovementAmount > account.ActualAmount)
            {
                Console.WriteLine("--> Not adding movement. Movement amount is greater than available money");
                throw new Exception("Movement is greater than available salary.");
            }
            account.ActualAmount += movement.MovementAmount;
            _context.Accounts.Update(account);
            _context.Movements.Add(movement);
            _context.SaveChanges();
            Console.WriteLine("--> Movement done.");

        }

        public IEnumerable<Movement> GetAllMovements(int accountId) =>
            _context.Movements.ToList();

        public Movement GetMovementById(int movementId, int accountId) =>
            _context.Movements
                .Where(m => m.Id == movementId && m.AccountId == accountId)
                .FirstOrDefault();

        public MovementType GetMovementType(int movementTypeId) =>
            _context.Types.FirstOrDefault(t => t.Id == movementTypeId);
    }


}