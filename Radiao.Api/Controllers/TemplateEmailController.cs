using AutoMapper;
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
    public class TemplateEmailController : MainController
    {
        private readonly IMapper _mapper;
        private readonly ITemplateEmailRepository _templateEmailRepository;
        private readonly ITemplateEmailService _templateEmailService;

        public TemplateEmailController(
            ILogger<TemplateEmailController> logger,
            INotifier notifier,
            IMapper mapper,
            ITemplateEmailRepository templateEmailRepository,
            ITemplateEmailService templateEmailService) : base(logger, notifier)
        {
            _mapper = mapper;
            _templateEmailRepository = templateEmailRepository;
            _templateEmailService = templateEmailService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TemplateEmailViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TemplateEmailViewModel>>> GetAll()
        {
            var templates = _mapper.Map<List<TemplateEmailViewModel>>(await _templateEmailRepository.GetAll());
            return CustomResponse(templates);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemplateEmailViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TemplateEmailViewModel>> Get(Guid id)
        {
            var template = await _templateEmailRepository.GetById(id);

            if (template == null)
            {
                return NotFound();
            }

            return CustomResponse(_mapper.Map<TemplateEmailViewModel>(template));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemplateEmailViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TemplateEmailViewModel>>> Post([FromBody] TemplateEmailViewModel templateEmailViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var template = await _templateEmailService.Create(_mapper.Map<TemplateEmail>(templateEmailViewModel));

            return CustomResponse(_mapper.Map<TemplateEmailViewModel>(template));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemplateEmailViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TemplateEmailViewModel>> Put(Guid id, [FromBody] TemplateEmailViewModel templateEmailViewModel)
        {
            if (templateEmailViewModel.Id != id)
            {
                Notify("Id informado não bate!");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var template = await _templateEmailService.Update(_mapper.Map<TemplateEmail>(templateEmailViewModel));

            return CustomResponse(_mapper.Map<TemplateEmailViewModel>(template));
        }
    }
}
