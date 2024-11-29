namespace TrisGPOI.Core.OTP.Interfaces
{
    public interface IOTPRepository
    {
        Task AddNewOTP(string email, string opt);
        Task<bool> CheckOTP(string email, string otp);
    }
}
