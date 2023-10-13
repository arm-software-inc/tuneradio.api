using Radiao.Domain.Entities;

namespace Radiao.Domain.Services
{
    public interface ITemplateEmailService
    {
        Task<TemplateEmail?> Create(TemplateEmail templateEmail);

        Task<TemplateEmail> Update(TemplateEmail templateEmail);
    }
}
