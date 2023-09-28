using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;

namespace Radiao.Domain.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Create(User user)
        {
            user.HashPassword();
            user.SetActive();

            var emailAlreadyInUse = await _userRepository.GetByEmail(user.Email);
            if (emailAlreadyInUse != null)
            {
                throw new Exception("email em uso");
            }

            await _userRepository.Create(user);

            return user;
        }

        public async Task<User> Update(User user)
        {
            return await _userRepository.Update(user);
        }
    }
}
