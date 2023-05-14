using System.ComponentModel.DataAnnotations;

namespace AccountService.Dtos
{
    public class IncreaseAmountDto
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public float IncreaseAmount { get; set; }
    }
}