using Radiao.Domain.Services.Notifications;

namespace Radiao.Domain.Services.Impl
{
    public abstract class ServiceBase
    {
        private readonly INotifier _notifier;

        protected ServiceBase(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string errorMessage)
        {
            _notifier.Handle(new Notification(errorMessage));
        }
    }
}
