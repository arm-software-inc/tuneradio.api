using Radiao.Domain.Entities;

namespace Radiao.Domain.Repositories
{
    public interface IFavoriteRepository
    {
        Task<Favorite> Create(Favorite favorite);

        Task Delete(Guid id);

        Task<Favorite?> Get(Guid id);

        Task<Favorite?> GetByUserAndStation(Guid userId, Guid stationId);

        Task<List<Favorite>> GetAll(Guid userId);
    }
}
