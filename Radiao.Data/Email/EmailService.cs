using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using Radiao.Domain.Enums;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services;

namespace Radiao.Data.Email
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly ITemplateEmailRepository _templateEmailRepository;

        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _smtpEmail;

        public EmailService(
            IConfiguration configuration,
            ITemplateEmailRepository templateEmailRepository,
            ILogger<EmailService> logger)
        {
            _smtpServer = configuration.GetSection("Smtp")["Server"]!;
            _smtpPort = int.Parse(configuration.GetSection("Smtp")["Port"]!);
            _smtpUsername = configuration.GetSection("Smtp")["Username"]!;
            _smtpPassword = configuration.GetSection("Smtp")["Password"]!;
            _smtpEmail = configuration.GetSection("Smtp")["Email"]!;
            _templateEmailRepository = templateEmailRepository;
            _logger = logger;
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

        public async Task SendRegistration(string destination, string username)
        {
            var templateEmail = await _templateEmailRepository
                .GetByType(TemplateEmailType.Welcome);

            if (templateEmail == null)
            {
                _logger.LogWarning("Não há template para o email de boas vindas!");
                return;
            }

            var template = templateEmail.Template.Replace("{USER_NAME}", username);

            await Send(destination, template, templateEmail.EmailSubject);

            _logger.LogInformation($"Email de boas vindas enviado para {destination}");
        }

        public async Task SendPasswordRecovery(string destination)
        {
            var templateEmail = await _templateEmailRepository
                .GetByType(TemplateEmailType.PasswordRecovery);

            if (templateEmail == null)
            {
                _logger.LogWarning("Não há template para o email de recuperação de senha!");
                return;
            }

            await Send(destination, templateEmail.Template, templateEmail.EmailSubject);

            _logger.LogInformation($"Email de recuperação de senha enviado para {destination}");
        }
    }
}
