using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Radiao.Api.Helpers;
using Radiao.Api.ViewModels;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services.Notifications;

namespace Radiao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthController(
            ILogger<AuthController> logger,
            INotifier notifier,
            IUserRepository userRepository,
            IConfiguration configuration,
            IMapper mapper) : base(logger, notifier)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// SignIn
        /// </summary>
        /// <param name="authViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResultViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Auth(AuthViewModel authViewModel)
        {
            var user = await _userRepository.GetByEmail(authViewModel.Email);

            if (user == null || !user.ValidatePassword(authViewModel.Password))
            {
                Notify("Usuário ou senha incorretos!");
                return CustomResponse();
            }

            var secret = _configuration.GetSection("JwtConfig").GetValue<string>("JwtSecret");
            var token = TokenHelper.GenerateToken(user, secret);

            return CustomResponse(new AuthResultViewModel
            {
                Token = token,
                User = _mapper.Map<UserViewModel>(user)
            });
        }

        /// <summary>
        /// Send an email to recover the user's password
        /// </summary>
        /// <param name="recoveryPassword">user's email</param>
        /// <returns></returns>
        [HttpPost("recovery")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RecoveryPassword([FromBody] RecoveryPasswordViewModel recoveryPassword)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            return Ok();
        }
    }
}
