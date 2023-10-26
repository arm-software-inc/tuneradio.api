using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;

namespace Radiao.Domain.Services.Impl
{
    public class UserHistoryService : IUserHistoryService
    {
        private readonly IUserHistoryRepository _userHistoryRepository;
        private readonly IStationRepository _stationRepository;

        public UserHistoryService(
            IUserHistoryRepository userHistoryRepository,
            IStationRepository stationRepository)
        {
            _userHistoryRepository = userHistoryRepository;
            _stationRepository = stationRepository;
        }

        public async Task<List<UserHistory>> GetByUserId(Guid userId)
        {
            var historyList = await _userHistoryRepository.GetByUserId(userId);

            foreach (var history in historyList)
            {
                var station = await _stationRepository
                    .Get(history.StationId.ToString());

                if (station != null)
                    history.SetStation(station);
            }

            return historyList;
        }
    }
}
