using Radiao.Domain.Entities;

namespace Radiao.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);

        Task<User> Update(User user);

        Task UpdatePassword(Guid userId, string password);

        Task Delete(Guid id);

        Task<User?> Get(Guid id);

        Task<List<User>> GetAll();

        Task<User?> GetByEmail(string email);
    }
}
