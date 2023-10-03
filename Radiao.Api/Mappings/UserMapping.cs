using AutoMapper;
using Radiao.Api.ViewModels;
using Radiao.Domain.Entities;

namespace Radiao.Api.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(user => user.Password, opt => opt.Ignore());
            
            CreateMap<UserViewModel, User>();

            CreateMap<EditUserViewModel, User>();
        }
    }
}
