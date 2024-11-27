using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace TrisGPOI.Controllers.User.Entities
{
    public class UserIdentifier
    {
        public required string EmailOrUsername { get; set; }
        public required string Password { get; set; }
    }
}