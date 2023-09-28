using Radiao.Domain.Entities;

namespace Radiao.Domain.Repositories
{
    public interface IStationRepository
    {

        Task<Station?> Get(Guid id);

        Task<List<Station>> GetAll();

        Task<List<Station>> GetByPopularity();

        Task<List<Station>> GetByCategory();
    }
}
