using AccountService.Models;

namespace AccountService.Repositories
{
    public interface ICoinTypeRepo
    {
        IEnumerable<CoinType> GetAllCoins();

        CoinType GetCoinById(int id);

        void CreateCoin(CoinType coin);
    }
}