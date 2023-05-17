using System.ComponentModel.DataAnnotations;

namespace MovementService.Dtos
{
    public class TypeAddDto
    {
        [Required]
        public String Name { get; set; }
    }
}