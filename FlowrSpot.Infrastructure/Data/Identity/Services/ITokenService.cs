using FlowrSpot.Application.Users.Dtos;

namespace FlowrSpot.Infrastructure.Data.Identity.Services
{
    public interface ITokenService
    {
        string Generate(UserDto user);
    }
}
