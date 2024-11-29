namespace TrisGPOI.Core.OTP.Interfaces
{
    public interface IOTPManager
    {
        string GenerateOtp();
        Task AddNewOTP(string email, string otp);
        Task CheckOTP(string email, string otp);
    }
}
