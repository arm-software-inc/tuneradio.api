using Microsoft.Extensions.Configuration;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;

namespace Radiao.Data.RadioBrowser
{
    public class StationRepository : IStationRepository
    {
        public Task<Station?> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Station>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Station>> GetByCategory()
        {
            throw new NotImplementedException();
        }

        public Task<List<Station>> GetByPopularity()
        {
            throw new NotImplementedException();
        }
    }
}
