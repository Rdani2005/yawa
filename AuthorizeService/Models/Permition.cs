using System.ComponentModel.DataAnnotations;

namespace AuthorizeService.Models
{
    public class Permition
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<PermitionRol> PermitionRols { get; set; }
    }
}