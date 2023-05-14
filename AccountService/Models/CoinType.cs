using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class CoinType
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}