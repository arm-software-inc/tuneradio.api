﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(await _stationRepository.GetAll());
        }

        [HttpGet("trending")]
        public async Task<ActionResult> GetTrending()
        {
            return CustomResponse(await _stationRepository.GetTrending());
        }

        [HttpGet("popular")]
        public async Task<ActionResult> GetPopular()
        {
            return CustomResponse(await _stationRepository.GetPopular());
        }

        [HttpGet("category")]
        public async Task<ActionResult> GetByCategory([FromQuery] string c)
        {
            return CustomResponse(await _stationRepository.GetByCategory(c));
        }
    }
}
