using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class Permition
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }
    }
}