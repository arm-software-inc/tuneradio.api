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
    public class TagController : MainController
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;
        private readonly ITagService _tagService;

        public TagController(
            ILogger<TagController> logger,
            INotifier notifier,
            ITagRepository tagRepository,
            IMapper mapper,
            ITagService tagService) : base(logger, notifier)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _tagService = tagService;
        }

        /// <summary>
        /// Fetch all of the tags
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TagViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TagViewModel>>> GetAll()
        {
            var tags = await _tagRepository.GetAll();

            return CustomResponse(_mapper.Map<List<TagViewModel>>(tags));
        }

        /// <summary>
        /// Get the tag by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TagViewModel>>> Get(Guid id)
        {
            var tags = await _tagRepository.GetById(id);

            if (tags == null)
            {
                return NotFound();
            }

            return CustomResponse(_mapper.Map<List<TagViewModel>>(tags));
        }

        /// <summary>
        /// Get the tag by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TagViewModel>>> Get(string name)
        {
            var tags = await _tagRepository.GetByName(name);

            if (tags == null)
            {
                return NotFound();
            }

            return CustomResponse(_mapper.Map<List<TagViewModel>>(tags));
        }

        /// <summary>
        /// Create a new tag
        /// </summary>
        /// <param name="tagViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TagViewModel>> Post([FromBody] TagViewModel tagViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var tag = await _tagService.Create(_mapper.Map<Tag>(tagViewModel));

            return CustomResponse(_mapper.Map<TagViewModel>(tag));
        }

        /// <summary>
        /// Update an existing tag
        /// </summary>
        /// <param name="tagViewModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TagViewModel>> Post([FromBody] TagViewModel tagViewModel, Guid id)
        {
            if (tagViewModel.Id != id)
            {
                Notify("O Id não bate os dados");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var tag = await _tagService.Update(_mapper.Map<Tag>(tagViewModel));

            return CustomResponse(_mapper.Map<TagViewModel>(tag));
        }

        /// <summary>
        /// Delete an existing tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _tagRepository.Delete(id);

            return Ok();
        }
    }
}
