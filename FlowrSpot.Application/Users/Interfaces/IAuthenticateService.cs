using FlowrSpot.Application.Users.Dtos;

namespace FlowrSpot.Application.Users.Interfaces
{
    public interface IAuthenticateService
    {
        Task<string> Authenticate(UserDto user);
    }
}
