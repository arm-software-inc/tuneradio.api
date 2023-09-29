using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services.Notifications;

namespace Radiao.Domain.Services.Impl
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IUserRepository userRepository,
            INotifier notifier) : base(notifier)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Create(User user)
        {
            user.HashPassword();
            user.SetActive();

            var emailAlreadyInUse = await _userRepository.GetByEmail(user.Email);
            if (emailAlreadyInUse != null)
            {
                Notify("Este email já está sendo usado!");
                return null;
            }

            await _userRepository.Create(user);

            return user;
        }

        public async Task<User> Update(User user)
        {
            return await _userRepository.Update(user);
        }

        public async Task UpdatePassword(Guid userId, string password)
        {
            var user = await _userRepository.Get(userId);

            if (user == null)
            {
                return;
            }

            user.ChangeAndHashPassword(password);

            await _userRepository.UpdatePassword(userId, user.Password);
        }
    }
}
