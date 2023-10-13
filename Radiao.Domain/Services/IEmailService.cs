namespace Radiao.Domain.Services
{
    public interface IEmailService
    {
        Task Send(string destination, string htmlBody, string subject);

        Task SendRegistration(string destination, string username);

        Task SendPasswordRecovery(string destination);
    }
}
