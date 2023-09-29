namespace Radiao.Domain.Services.Notifications
{
    public interface INotifier
    {
        bool HasErrors();

        List<Notification> GetErrors();

        void Handle(Notification notification);
    }
}
