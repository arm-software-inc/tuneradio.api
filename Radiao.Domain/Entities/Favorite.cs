namespace Radiao.Domain.Entities
{
    public class Favorite : Entity
    {
        public int UserId { get; private set; }

        public Guid StationId { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        private Favorite()
        {}

        public Favorite(int userId, Guid stationId)
        {
            UserId = userId;
            StationId = stationId;
        }
    }
}
