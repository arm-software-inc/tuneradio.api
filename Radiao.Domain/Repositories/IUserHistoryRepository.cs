using Radiao.Domain.Entities;

namespace Radiao.Domain.Repositories
{
    public interface IUserHistoryRepository
    {
        Task<UserHistory> Create(UserHistory userHistory);

        Task Delete(Guid id);

        Task DeleteByUserId(Guid userId);

        Task<UserHistory> Get(Guid id);

        Task<List<UserHistory>> GetByUserId(Guid userId);
    }
}
