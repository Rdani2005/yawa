using System.ComponentModel.DataAnnotations;

namespace MovementService.Models
{
    public class Movement
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public float MovementAmount { get; set; }

        [Required]
        public String MovementDescription { get; set; }

        [Required]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [Required]
        public int TypeId { get; set; }
        public MovementType Type { get; set; }

        public DateTime DoneDate { get; set; } = DateTime.Now;
    }
}