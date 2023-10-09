namespace Radiao.Domain.Services
{
    public interface IEmailService
    {
        Task Send(string destination, string htmlBody, string subject);
    }
}
