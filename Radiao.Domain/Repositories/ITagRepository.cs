using Radiao.Domain.Entities;

namespace Radiao.Domain.Repositories
{
    public interface ITagRepository
    {
        Task<Tag> GetById(Guid id);

        Task<Tag> GetByName(string name);

        Task<List<Tag>> GetAll();

        Task<Tag> Create(Tag tag);

        Task<Tag> Update(Tag tag);

        Task Delete(Guid id);
    }
}
