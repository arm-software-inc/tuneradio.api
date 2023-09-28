using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radiao.Api.ViewModels;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services;

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
            IUserService userService,
            IMapper mapper,
            IUserRepository userRepository) : base(logger)
        {
            _userService = userService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = await _userRepository.Get(GetUserId());

            if (user == null)
            {
                return NotFound();
            }

            return CustomResponse(_mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] UserViewModel userViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);

            return CustomResponse(_mapper.Map<UserViewModel>(await _userService.Create(user)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] EditUserViewModel userViewModel, int id)
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

        [HttpDelete("{id}")]
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
