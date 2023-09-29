using Microsoft.AspNetCore.Mvc;
using Radiao.Api.ViewModels;
using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services.Notifications;

namespace Radiao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationController : MainController
    {
        private readonly IStationRepository _stationRepository;

        public StationController(
            ILogger<StationController> logger,
            INotifier notifier,
            IStationRepository stationRepository) : base(logger, notifier)
        {
            _stationRepository = stationRepository;
        }

        /// <summary>
        /// Fetch all of the stations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Station>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Station>>> GetAll()
        {
            return CustomResponse(await _stationRepository.GetAll());
        }

        /// <summary>
        /// Fetch trending stations
        /// </summary>
        /// <returns></returns>
        [HttpGet("trending")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Station>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Station>>> GetTrending()
        {
            return CustomResponse(await _stationRepository.GetTrending());
        }

        /// <summary>
        /// Fetch stations by popularity
        /// </summary>
        /// <returns></returns>
        [HttpGet("popular")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Station>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Station>>> GetPopular()
        {
            return CustomResponse(await _stationRepository.GetPopular());
        }

        /// <summary>
        /// Fetch stations by category
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpGet("category")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Station>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Station>>> GetByCategory([FromQuery] string c)
        {
            return CustomResponse(await _stationRepository.GetByCategory(c));
        }
    }
}
