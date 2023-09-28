using AutoMapper;
using Radiao.Api.ViewModels;
using Radiao.Domain.Entities;

namespace Radiao.Api.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<EditUserViewModel, User>();
        }
    }
}
