using TrisGPOI.Core.User.Entities;

namespace TrisGPOI.Core.User.Interfaces
{
    public interface IUserManager
    {
        Task RegisterAsync(UserRegister model);
        Task<string?> LoginAsync(UserLogin model);
        Task<string?> VerifyOTP(string otp, string email);
    }
}
