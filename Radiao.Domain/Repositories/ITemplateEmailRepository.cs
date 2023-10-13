using Radiao.Domain.Entities;
using Radiao.Domain.Enums;

namespace Radiao.Domain.Repositories
{
    public interface ITemplateEmailRepository
    {
        Task<TemplateEmail> Create(TemplateEmail templateEmail);

        Task<TemplateEmail> Update(TemplateEmail templateEmail);

        Task Delete(Guid id);

        Task<TemplateEmail?> GetById(Guid id);

        Task<TemplateEmail?> GetByType(TemplateEmailType templateEmailType);

        Task<List<TemplateEmail>> GetAll();
    }
}
