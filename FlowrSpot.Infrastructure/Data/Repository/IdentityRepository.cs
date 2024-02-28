using FlowrSpot.Application.Common.Enums;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Users.Dtos;
using FlowrSpot.Application.Users.Interfaces;
using FlowrSpot.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace FlowrSpot.Infrastructure.Data.Repository
{
    internal class IdentityRepository(IAuthenticateService authenticateService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IIdentityRepository
    {
        private readonly IAuthenticateService _authenticateService = authenticateService;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        public Task<string> AuthenticateAsync(UserDto userDto)
        {
            return _authenticateService.Authenticate(userDto);
        }

        public async Task<OperationResult<bool>> CreateUserAsync(UserDto userDto, string password)
        {
            var _result = new OperationResult<bool>();

            var appUser = new ApplicationUser { Email = userDto.Email, UserName = userDto.UserName };
            var createdUser = await _userManager.CreateAsync(appUser, password);
            if (!createdUser.Succeeded)
            {
                foreach (var identityError in createdUser.Errors)
                {
                    _result.AddError(ErrorCode.CreateError, identityError.Description);
                }
                return _result;
            }
            else
            {
                userDto.Id = appUser.Id;
            }
            _result.Data = true;
            return _result;
        }

        public async Task<bool> PasswordSignInAsync(UserDto userDto, string password)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id!);
            var result = await _signInManager.PasswordSignInAsync(user!, password, false, false);
            return result.Succeeded;
        }

        public async Task<UserDto> FindByEmailAsync(string username)
        {
            var user = await _userManager.FindByEmailAsync(username) ?? throw new Exception("User not found");
            UserDto result = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };
            return result;
        }

       
    }
}
