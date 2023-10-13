using Radiao.Domain.Enums;

namespace Radiao.Domain.Entities
{
    public class TemplateEmail : Entity
    {
        public string Name { get; private set; }

        public string Template { get; private set; }

        public TemplateEmailType TemplateType { get; private set; }

        public string EmailSubject { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; }

        public DateTime UpdatedAt { get; }

        private TemplateEmail()
        {}

        public TemplateEmail(
            string name,
            string template,
            TemplateEmailType templateType,
            string emailSubject,
            bool isActive)
        {
            Name = name;
            Template = template;
            TemplateType = templateType;
            IsActive = isActive;
            EmailSubject = emailSubject;
        }

        public void Inactive()
        {
            IsActive = false;
        }
    }
}
