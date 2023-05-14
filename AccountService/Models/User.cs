using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ExternalID { get; set; }

        [Required]
        public String identification { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}