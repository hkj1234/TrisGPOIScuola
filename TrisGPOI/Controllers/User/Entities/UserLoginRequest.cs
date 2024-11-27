using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using TrisGPOI.Core.User.Entities;

namespace TrisGPOI.Controllers.User.Entities
{
    public class UserLoginRequest
    {
        public required string EmailOrUsername { get; set; }
        public required string Password { get; set; }

        public UserLogin ToUserLogin()
        {
            return new UserLogin { 
                EmailOrUsername = EmailOrUsername,
                Password = Password
            };
        }
    }
}