namespace AccountService.Dtos
{
    public class AccountReadDto
    {

        public int Id { get; set; }
        public float InitialAmount { get; set; }
        public float ActualAmount { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddedDay { get; set; }

        public TypeReadDto Type { get; set; }
        public CoinReadDto Coin { get; set; }
    }
}