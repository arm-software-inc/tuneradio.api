using Radiao.Domain.Enums;

namespace Radiao.Api.ViewModels
{
    public class TemplateEmailViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Template { get; set; }

        public TemplateEmailType TemplateType { get; set; }

        public string EmailSubject { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
