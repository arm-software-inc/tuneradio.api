namespace Radiao.Domain.Services
{
    public interface IUserRecovery
    {
        Task RecoveryPassword(string userEmail);
    }
}
