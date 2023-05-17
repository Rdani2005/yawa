using System.ComponentModel.DataAnnotations;

namespace MovementService.Models
{
    public class MovementType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public ICollection<Movement> Movements { get; set; } = new List<Movement>();
    }
}