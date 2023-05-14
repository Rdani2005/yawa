using AccountService.Data;
using AccountService.Models;

namespace AccountService.Repositories
{
    public class CoinTypeRepo : ICoinTypeRepo
    {
        private readonly AppDbContext _context;

        public CoinTypeRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCoin(CoinType coin)
        {
            if (coin == null) throw new ArgumentNullException(nameof(coin));
            _context.Coins.Add(coin);
            _context.SaveChanges();
        }

        public IEnumerable<CoinType> GetAllCoins()
        {
            return _context.Coins.ToList();
        }

        public CoinType GetCoinById(int id)
        {
            return _context.Coins.FirstOrDefault(
                c => c.Id == id
            );
        }
    }
}