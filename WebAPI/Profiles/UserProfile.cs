using AutoMapper;
using DataAccess.Entities;
using Services.DTOs;

namespace Stock_Trading_System.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UpdateUserDto, ApplicationUser>();
        }
    }
}
