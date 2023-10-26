namespace Radiao.Domain.Entities
{
    public class UserHistory : Entity
    {
        public Guid UserId { get; private set; }

        public Guid StationId { get; private set; }

        public DateTime CreatedAt { get; }

        public Station? Station { get; private set; }

        private UserHistory()
        {}

        public UserHistory(
            Guid userId,
            Guid stationId)
        {
            UserId = userId;
            StationId = stationId;
            CreatedAt = DateTime.Now;
        }

        public void SetStation(Station station)
        {
            Station = station;
        }
    }
}
