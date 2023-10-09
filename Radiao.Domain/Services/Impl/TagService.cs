using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services.Notifications;

namespace Radiao.Domain.Services.Impl
{
    public class TagService : ServiceBase, ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(
            INotifier notifier,
            ITagRepository tagRepository) : base(notifier)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Tag?> Create(Tag tag)
        {
            var storedTag = await _tagRepository.GetByName(tag.Name);

            if (storedTag != null)
            {
                Notify("Já existe uma tag com este nome!");
                return null;
            }

            return await _tagRepository.Create(tag);
        }

        public async Task<Tag?> Update(Tag tag)
        {
            var storedTag = await _tagRepository.GetByName(tag.Name);

            if (storedTag != null)
            {
                Notify("Já existe uma tag com este nome!");
                return null;
            }

            return await _tagRepository.Update(tag);
        }
    }
}
