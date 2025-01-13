using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.OTP.Entities
{
    public class DBOtpEntity
    {
        [Key]
        [ForeignKey(nameof(DBUser))]
        public string Email { get; set; } = "";
        [Required]
        public string OtpCode { get; set; } = "";
        [Required]
        public DateTime ExpiryTime { get; set; }
    }
}
