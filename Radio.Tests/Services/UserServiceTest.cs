using Moq;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services;
using Radiao.Domain.Services.Impl;

namespace Radio.Tests.Services
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            var userRepository = new Mock<IUserRepository>();
            var roleRepository = new Mock<IRoleRepository>();

            _userService = new UserService(userRepository.Object, roleRepository.Object);
        }

        [Fact]
        public async Task CreateUser()
        {
            var user = new User("abel@abel", "testedasenha", "abel");
            
            await _userService.Create(user);

            Assert.NotNull(user);
        }

        [Fact]
        public async Task CreateUser_ShouldEncryptPassword()
        {
            var user = new User("abel@abel", "testedasenha", "abel");

            await _userService.Create(user);

            Assert.True(user.ValidatePassword("testedasenha"));
        }
    }
}
