using System.ComponentModel.DataAnnotations;

namespace AuthService.Dtos
{
    public class RolAddDto
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

    }
}