using System.ComponentModel.DataAnnotations;

namespace AccountService.Dtos
{
    public class AccountAddDto
    {
        [Required]
        public float InitialAmount { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int CoinId { get; set; }
    }
}