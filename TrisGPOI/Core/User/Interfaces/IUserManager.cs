using TrisGPOI.Core.User.Entities;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Core.User.Interfaces
{
    public interface IUserManager
    {
        Task ChangeUserPassword(string email, string password);
        Task RegisterAsync(UserRegister model);
        Task<string> LoginAsync(UserLogin model);
        Task<string> VerifyOTP(string otp, string email);
        Task PasswordDimenticata(string email);
        Task LoginOTP(string email);
        Task<UserData> GetUserData(string email);
    }
}
