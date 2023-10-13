using AutoMapper;
using Radiao.Api.ViewModels;
using Radiao.Domain.Entities;

namespace Radiao.Api.Mappings
{
    public class TemplateEmailMapping : Profile
    {
        public TemplateEmailMapping()
        {
            CreateMap<TemplateEmail, TemplateEmailViewModel>()
                .ReverseMap();
        }
    }
}
