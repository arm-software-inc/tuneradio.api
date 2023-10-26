using Radiao.Domain.Entities;

namespace Radiao.Domain.Services
{
    public interface IUserHistoryService
    {
        Task<List<UserHistory>> GetByUserId(Guid userId);
    }
}
