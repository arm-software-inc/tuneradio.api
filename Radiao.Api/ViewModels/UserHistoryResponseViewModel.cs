using Radiao.Domain.Entities;

namespace Radiao.Api.ViewModels
{
    public class UserHistoryResponseViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid StationId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Station? Station { get; set; }
    }
}
