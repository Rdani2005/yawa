using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class Account
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public float InitialAmount { get; set; }

        [Required]
        public float ActualAmount { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
        public DateTime AddedDay { get; set; } = DateTime.Now;


        [Required]
        public int UserId { get; set; }
        public User User { get; set; }


        [Required]
        public int TypeId { get; set; }
        public AccountType Type { get; set; }


        [Required]
        public int CoinId { get; set; }
        public CoinType Coin { get; set; }



    }
}