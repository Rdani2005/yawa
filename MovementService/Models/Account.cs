using System.ComponentModel.DataAnnotations;

namespace MovementService.Models
{
    public class Account
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public float ActualAmount { get; set; }

        [Required]
        public int ExternalId { get; set; }
        public ICollection<Movement> Movements { get; set; } = new List<Movement>();
    }
}