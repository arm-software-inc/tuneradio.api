namespace Radiao.Domain.Entities
{
    public class Favorite : Entity
    {
        public Guid UserId { get; private set; }

        public Guid StationId { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        private Favorite()
        {}

        public Favorite(Guid userId, Guid stationId)
        {
            UserId = userId;
            StationId = stationId;
        }
    }
}
