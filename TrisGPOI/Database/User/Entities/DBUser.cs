using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace TrisGPOI.Database.User.Entities
{
    public class DBUser
    {
        [Key]
        public string Email { get; set; } = "";
        [Key]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        public bool IsActive { get; set; }
        public string Status { get; set; } = "Offline";
        public int StatusNumber { get; set; } = 0;
        //da fare, da fare foreighkey
        public string FotoProfilo { get; set; } = "Default";
        public string Description { get; set; } = "";
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;

        //data:

        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;

        public int MoneyXO { get; set; } = 0;

        public int RewardRemain { get; set; } = 0;
    }
}