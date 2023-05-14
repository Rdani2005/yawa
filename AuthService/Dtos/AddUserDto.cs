using System.ComponentModel.DataAnnotations;

namespace AuthService.Dtos
{
    public class AddUserDto
    {
        [Required]
        public String Identification { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}