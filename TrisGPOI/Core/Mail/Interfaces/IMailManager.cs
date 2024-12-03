namespace TrisGPOI.Core.Mail.Interfaces
{
    public interface IMailManager
    {
        Task SendRegisterOtpEmailAsync(string toEmail);
        Task SendPasswordDimenticataEmailAsync(string toEmail, string password);
        Task SendLoginOtpEmailAsync(string toEmail);
    }
}
