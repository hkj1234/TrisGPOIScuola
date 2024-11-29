namespace TrisGPOI.Core.Mail.Interfaces
{
    public interface IMailManager
    {
        Task SendOtpEmailAsync(string toEmail);
        Task SendPasswordDimenticataEmailAsync(string toEmail, string password);
    }
}
