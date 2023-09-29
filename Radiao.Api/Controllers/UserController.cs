using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radiao.Api.ViewModels;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services;
using Radiao.Domain.Services.Notifications;

namespace Radiao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(
            ILogger<UserController> logger,
            INotifier notifier,
            IUserService userService,
            IMapper mapper,
            IUserRepository userRepository) : base(logger, notifier)
        {
            _userService = userService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get the authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserViewModel>> Get()
        {
            var user = await _userRepository.Get(GetUserId());

            if (user == null)
            {
                return NotFound();
            }

            return CustomResponse(_mapper.Map<UserViewModel>(user));
        }

        /// <summary>
        /// SignUp
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserViewModel>> Post([FromBody] UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var user = _mapper.Map<User>(userViewModel);

            return CustomResponse(_mapper.Map<UserViewModel>(await _userService.Create(user)));
        }

        /// <summary>
        /// Updates user`s account
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserViewModel>> Put([FromBody] EditUserViewModel userViewModel, int id)
        {
            if (userViewModel.Id != id)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userViewModel);

            await _userRepository.Update(user);

            user = await _userRepository.Get(id);

            return CustomResponse(_mapper.Map<UserViewModel>(user));
        }

        /// <summary>
        /// Disable user's account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            await _userRepository.Delete(id);

            return CustomResponse();
        }

        [HttpGet("favorite")]
        public async Task<ActionResult> GetFavorites()
        {
            return Ok();
        }

        [HttpPost("favorite/{stationid:guid}")]
        public async Task<ActionResult> PostFavorite()
        {
            return Ok();
        }

        [HttpDelete("favorite/{stationid:guid}")]
        public async Task<ActionResult> DeleteFavorite()
        {
            return Ok();
        }
    }
}
