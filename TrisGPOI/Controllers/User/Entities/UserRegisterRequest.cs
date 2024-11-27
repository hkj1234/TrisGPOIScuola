using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using TrisGPOI.Core.User.Entities;

namespace TrisGPOI.Controllers.User.Entities
{
    public class UserRegisterRequest
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public UserRegister ToUserRegister()
        {
            return new UserRegister
            { 
                Email = Email,
                Username = Username,
                Password = Password
            };
        }
    }
}