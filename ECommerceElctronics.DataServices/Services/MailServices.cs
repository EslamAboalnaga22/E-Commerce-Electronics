using ECommerceElctronics.Entities.Dtos.Account;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using ECommerceElctronics.Entities.Models;

namespace ECommerceElctronics.DataServices.Services
{
    public class MailServices : IMailServices
    {
        private readonly EmailConfiguration emailConfiguration;
        public MailServices(IOptions<EmailConfiguration> options)
        {
            emailConfiguration = options.Value;
        }
        public async Task<bool> SendMailAsync(string email, string token)
        {
            MimeMessage emailMassage = new ();

            MailboxAddress emailForm = new(emailConfiguration.Name, emailConfiguration.EmailId);
            emailMassage.From.Add(emailForm);

            MailboxAddress emailTo = new(email, email);
            emailMassage.To.Add(emailTo);

            emailMassage.Subject = "Reset Password Token";

            BodyBuilder emailBodyBuilder = new();
            emailBodyBuilder.TextBody = token;
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
