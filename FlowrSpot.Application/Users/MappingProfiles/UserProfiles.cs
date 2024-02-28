using AutoMapper;
using FlowrSpot.Application.Users.Command;
using FlowrSpot.Application.Users.Dtos;

namespace FlowrSpot.Application.Users.MappingProfiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<LoginDto, LoginCommand>().ReverseMap();
            CreateMap<RegisterDto, RegisterCommand>().ReverseMap();
            CreateMap<UserDto, RegisterCommand>().ReverseMap();
            CreateMap<UserDto, AuthResponseDto>().ReverseMap();
        }
    }
}
