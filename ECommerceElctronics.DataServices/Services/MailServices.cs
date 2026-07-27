using ECommerceElctronics.Entities.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ECommerceElctronics.DataServices.Services
{
    public class MailServices(IOptions<EmailConfiguration> options) : IMailServices
    {
        private readonly EmailConfiguration emailConfiguration = options.Value;

        public async Task<bool> SendMailAsync(string email, string token)
        {
            MimeMessage emailMassage = new ();

            MailboxAddress emailForm = new(emailConfiguration.Name, emailConfiguration.EmailId);
            emailMassage.From.Add(emailForm);

            MailboxAddress emailTo = new(email, email);
            emailMassage.To.Add(emailTo);

            emailMassage.Subject = "Reset Password Token";

            BodyBuilder emailBodyBuilder = new()
            {
                TextBody = token
            };

            emailMassage.Body = emailBodyBuilder.ToMessageBody();

            // SmtpClient Class form Mailkit
            SmtpClient smtpClient = new();
            smtpClient.Connect(emailConfiguration.Host, emailConfiguration.Port, emailConfiguration.UseSSL);
            smtpClient.Authenticate(emailConfiguration.EmailId, emailConfiguration.Password);
            smtpClient.Send(emailMassage);
            smtpClient.Disconnect(true);
            smtpClient.Dispose();

            return true;
        }
    }
}
