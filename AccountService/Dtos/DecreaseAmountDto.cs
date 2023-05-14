using System.ComponentModel.DataAnnotations;

namespace AccountService.Dtos
{
    public class DecreaseAmountDto
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public float IncreaseAmount { get; set; }
    }
}