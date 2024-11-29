namespace TrisGPOI.Core.Mail.Interfaces
{
    public interface IMailManager
    {
        Task SendOtpEmailAsync(string toEmail);
    }
}
