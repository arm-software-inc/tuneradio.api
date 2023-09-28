using Radiao.Domain.Entities;

namespace Radiao.Domain.Repositories
{
    public interface IFavoriteRepository
    {
        Task<Favorite> Create(Favorite favorite);

        Task Delete(Guid id);

        Task<Favorite?> Get(Guid id);

        Task<List<Favorite>> GetAll();
    }
}
