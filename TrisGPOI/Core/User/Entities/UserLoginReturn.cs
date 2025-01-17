using System.Reflection.Metadata;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Core.User.Entities
{
    public class UserLoginReturn
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
