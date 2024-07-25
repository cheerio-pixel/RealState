using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RealState.Application.DTOs.EmailServices;
using RealState.Application.Interfaces.Services;
using RealState.Domain.Settings;

namespace RealState.Infrastructure.Shared.Services
{
    public class EmailServices(IOptions<EmailSettings> emailSettings) : IEmailServices
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;

        public async Task<bool> SendAsync(EmailRequestDTO request)
        {
            #region create email
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.EmailFrom);
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            var bodyBuilder = new BodyBuilder() { HtmlBody = request.Body };
            email.Body = bodyBuilder.ToMessageBody();
            #endregion

            #region send email
            try
            {
                using var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(_emailSettings.SmtpHost, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPassword);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            #endregion
        }
    }
}
