using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Users.Dtos;

namespace FlowrSpot.Application.Users.Interfaces
{
    public interface IIdentityRepository
    {
        Task<OperationResult<bool>> CreateUserAsync(UserDto userDto, string password);
        Task<string> AuthenticateAsync(UserDto userDto);
        Task<bool> PasswordSignInAsync(UserDto userDto, string password);
        Task<UserDto> FindByEmailAsync(string username);

    }
}
