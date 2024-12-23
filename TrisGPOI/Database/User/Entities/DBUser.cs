using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool IsActive { get; set; }
        public string Status { get; set; } = "";
        public string FotoProfilo { get; set; } = "";
        public string Description { get; set; } = "";
    }
}