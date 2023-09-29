using Radiao.Domain.Entities;

namespace Radiao.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);

        Task<User> Update(User user);

        Task UpdatePassword(int userId, string password);

        Task Delete(int id);

        Task<User?> Get(int id);

        Task<List<User>> GetAll();

        Task<User?> GetByEmail(string email);
    }
}
