namespace Radiao.Domain.Services.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new();
        }

        public List<Notification> GetErrors()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasErrors()
        {
            return _notifications.Any();
        }
    }
}
