﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthController(
            ILogger<AuthController> logger,
            INotifier notifier,
            IUserRepository userRepository,
            IConfiguration configuration) : base(logger, notifier)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost]
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

            return CustomResponse(new { token, user });
        }

        [HttpPost("recovery")]
        public async Task<ActionResult> RecoveryPassword(string email)
        {
            return Ok();
        }
    }
}
