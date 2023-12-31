﻿using AutoMapper;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Radiao.Api.Helpers;
using Radiao.Api.ViewModels;
using Radiao.Domain.Options;
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
        private readonly GoogleAuthenticationOptions _googleAuthenticationOptions;
        private readonly string _secret;

        public AuthController(
            ILogger<AuthController> logger,
            INotifier notifier,
            IUserRepository userRepository,
            IConfiguration configuration,
            IMapper mapper,
            IOptions<GoogleAuthenticationOptions> googleAuthenticationOptions) : base(logger, notifier)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;

            _secret = _configuration.GetSection("JwtConfig").GetValue<string>("JwtSecret")!;
            _googleAuthenticationOptions = googleAuthenticationOptions.Value;
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

            var token = TokenHelper.GenerateToken(user, _secret);

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

        /// <summary>
        /// Valida o token de autenticação do Google
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost("signin-google")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResultViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GoogleSignIn([FromForm] string code)
        {
            var authorizationCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientSecret = _googleAuthenticationOptions.ClientSecret,
                    ClientId = _googleAuthenticationOptions.ClientId,
                }
            });

            var tokenResponse = await authorizationCodeFlow.ExchangeCodeForTokenAsync(
                "",
                code,
                redirectUri: _googleAuthenticationOptions.RedirectUrl,
                CancellationToken.None);

            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(tokenResponse.IdToken, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _googleAuthenticationOptions.ClientId },
            });

            if (payload == null)
            {
                Notify("Não foi possível autenticar com o Google");
                return CustomResponse();
            }

            var user = await _userRepository.GetByEmail(payload.Email);

            if (user == null)
            {
                user = await _userRepository.Create(new Domain.Entities.User(payload.Email, "", payload.Name));
            }

            var token = TokenHelper.GenerateToken(user, _secret);

            return Ok(new AuthResultViewModel
            {
                Token = token,
                User = _mapper.Map<UserViewModel>(user)
            });

        }
    }
}
