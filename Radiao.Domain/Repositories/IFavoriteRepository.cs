using Radiao.Domain.Entities;

namespace Radiao.Domain.Repositories
{
    public interface IFavoriteRepository
    {
        Task<Favorite> Create(Favorite favorite);

        Task Delete(int id);

        Task<Favorite?> Get(int id);

        Task<Favorite?> GetByUserAndStation(int userId, Guid stationId);

        Task<List<Favorite>> GetAll(int userId);
    }
}
