using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Radiao.Domain.Services;

namespace Radiao.Data.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _smtpEmail;

        public EmailService(IConfiguration configuration)
        {
            _smtpServer = configuration.GetSection("Smtp")["Server"]!;
            _smtpPort = int.Parse(configuration.GetSection("Smtp")["Port"]!);
            _smtpUsername = configuration.GetSection("Smtp")["Username"]!;
            _smtpPassword = configuration.GetSection("Smtp")["Password"]!;
            _smtpEmail = configuration.GetSection("Smtp")["Email"]!;
        }

        public async Task Send(string destination, string htmlBody, string subject)
        {
            var body = new Multipart
            {
                new TextPart(TextFormat.Html){ Text = htmlBody },
            };

            var email = new MimeMessage
            {
                Subject = subject,
                Body = body
            };

            email.From.Add(MailboxAddress.Parse(_smtpEmail));

            email.To.Add(MailboxAddress.Parse(destination));

            using var smtp = new SmtpClient();
            
            await smtp.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_smtpUsername, _smtpPassword);
            
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(quit: true);
        }

        public async Task SendWelcomeEmail(string email, string template)
        {
            await Send(email, template, "Bem vindo ao Radião");
        }

        public async Task SendRecoveryPasswordEmail(string email, string template)
        {
            await Send(email, template, "Recuperação de senha");
        }
    }
}
