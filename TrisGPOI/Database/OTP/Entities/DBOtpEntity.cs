using System.ComponentModel.DataAnnotations;

namespace TrisGPOI.Database.OTP.Entities
{
    public class DBOtpEntity
    {
        [Key]
        public string Email { get; set; } = "";
        [Required]
        public string OtpCode { get; set; } = "";
        [Required]
        public DateTime ExpiryTime { get; set; }
    }
}
