using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;

namespace Radiao.Data.MySql
{
    public class FavoriteRepository : IFavoriteRepository
    {
        public Task<Favorite> Create(Favorite favorite)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Favorite?> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Favorite>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
