using AutoMapper;
using Radiao.Api.ViewModels;
using Radiao.Domain.Entities;

namespace Radiao.Api.Mappings
{
    public class TagMapping : Profile
    {
        public TagMapping()
        {
            CreateMap<Tag, TagViewModel>().ReverseMap();
        }
    }
}
