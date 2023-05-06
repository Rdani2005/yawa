using System.ComponentModel.DataAnnotations;

namespace AuthorizeService.Models
{
    public class PermitionRol
    {
        [Required]
        public Permition Permition { get; set; }

        [Required]
        public int PermitionId { get; set; }

        [Required]
        public Rol Rol { get; set; }

        [Required]
        public int RolId { get; set; }
    }
}