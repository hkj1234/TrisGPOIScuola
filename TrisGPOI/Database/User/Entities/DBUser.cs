using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace TrisGPOI.Database.User.Entities
{
    public class DBUser
    {
        [Key]
        public string Email { get; set; } = "";
        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
    }
}