using Radiao.Domain.Entities;

namespace Radiao.Domain.Services
{
    public interface IUserService
    {
        Task<User> Create(User user);
    }
}
