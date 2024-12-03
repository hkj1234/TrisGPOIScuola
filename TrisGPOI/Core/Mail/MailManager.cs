using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using TrisGPOI.Core.Mail.Entities;
using TrisGPOI.Core.Mail.Interfaces;
using TrisGPOI.Core.OTP.Interfaces;

namespace TrisGPOI.Core.Mail
{
    public class MailManager : IMailManager
    {
        private readonly IConfiguration _configuration;
        private readonly IOTPManager _oTPManager;

        public MailManager(IConfiguration configuration, IOTPManager oTPManager)
        {
            _configuration = configuration;
            _oTPManager = oTPManager;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("No-reply", emailSettings.SmtpUser));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(emailSettings.SmtpUser, emailSettings.SmtpPass);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendRegisterOtpEmailAsync(string toEmail)
        {
            var otp = _oTPManager.GenerateOtp();
            var subject = "Verifica il tuo account con il codice OTP";
            var message = $"Gentile {toEmail},\r\n\r\nPer completare la verifica del tuo account, utilizza il seguente codice OTP (One-Time Password):\r\n\r\n{otp}\r\n\r\nIl codice è valido per 10 minuti. Ti preghiamo di non condividere questo codice con nessuno per motivi di sicurezza.\r\n\r\nSe non hai richiesto questo codice, ignora questa email.\r\n\r\nGrazie per aver scelto il nostro servizio!\r\n\r\nCordiali saluti,\r\nXOregion\r\n";

            await SendEmailAsync(toEmail, subject, message);

            await _oTPManager.AddNewOTP(toEmail, otp);
        }

        public async Task SendLoginOtpEmailAsync(string toEmail)
        {
            var otp = _oTPManager.GenerateOtp();
            var subject = "Codice OTP per accedere al tuo account";
            var message = $"Gentile {toEmail},\r\n\r\nUtilizza il seguente codice OTP per accedere al tuo account:\r\n\r\n{otp}\r\n\r\nIl codice è valido per 10 minuti. Non condividere questo codice con nessuno.\r\n\r\nSe non hai richiesto questo codice, ignora questa email.\r\n\r\nCordiali saluti,\r\nXOregion\r\n";

            await SendEmailAsync(toEmail, subject, message);

            await _oTPManager.AddNewOTP(toEmail, otp);
        }

        public async Task SendPasswordDimenticataEmailAsync(string toEmail, string password)
        {
            var subject = "Avviso di modifica della password";
            var message = $"Gentile {toEmail},\r\n\r\nAbbiamo ricevuto una richiesta per reimpostare la tua password. La tua password è stata cambiata con successo. Se non hai richiesto questa modifica, ti preghiamo di contattarci immediatamente.\r\n\r\nNuova password: {password}\r\n\r\nSe hai richiesto tu stesso la modifica, non è necessaria alcuna azione ulteriore. Tuttavia, ti consigliamo di mantenere le tue informazioni di accesso sicure e di non condividerle con nessuno.\r\n\r\nSe hai domande o hai bisogno di assistenza, non esitare a contattarci.\r\n\r\nGrazie per la tua attenzione.\r\n\r\nCordiali saluti,\r\nXOregion\r\n";

            await SendEmailAsync(toEmail, subject, message);
        }
    }
}
