using Radiao.Domain.Entities;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services.Notifications;

namespace Radiao.Domain.Services.Impl
{
    public class TemplateEmailService : ServiceBase, ITemplateEmailService
    {
        private readonly ITemplateEmailRepository _templateEmailRepository;

        public TemplateEmailService(
            INotifier notifier,
            ITemplateEmailRepository templateEmailRepository) : base(notifier)
        {
            _templateEmailRepository = templateEmailRepository;
        }

        public async Task<TemplateEmail?> Create(TemplateEmail templateEmail)
        {
            var template = await _templateEmailRepository.GetByType(templateEmail.TemplateType);

            if (template != null)
            {
                template.Inactive();
                await _templateEmailRepository.Update(template);
            }            

            return await _templateEmailRepository.Create(templateEmail);
        }

        public async Task<TemplateEmail> Update(TemplateEmail templateEmail)
        {
            return await _templateEmailRepository.Update(templateEmail);
        }
    }
}
