using System.ComponentModel.DataAnnotations;

namespace MovementService.Dtos
{
    public class MovementAddDto
    {
        [Required]
        public float MovementAmount { get; set; }

        [Required]
        public String MovementDescription { get; set; }

        [Required]
        public int TypeId { get; set; }
    }
}