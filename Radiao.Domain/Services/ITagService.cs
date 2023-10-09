using Radiao.Domain.Entities;

namespace Radiao.Domain.Services
{
    public interface ITagService
    {
        Task<Tag?> Create(Tag tag);

        Task<Tag?> Update(Tag tag);
    }
}
