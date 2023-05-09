using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public String Identification { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public DateTime CreatedDay { get; set; } = DateTime.Now;
    }
}