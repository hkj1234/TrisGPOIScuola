namespace TrisGPOI.Core.User.Entities
{
    public class UserLogin
    {
        public required string EmailOrUsername { get; set; }
        public required string Password { get; set; }
    }
}
