using FlowrSpot.Application.Users.Dtos;
using FlowrSpot.Application.Users.Interfaces;


namespace FlowrSpot.Infrastructure.Data.Identity.Services
{
    internal sealed class AuthenticateService(IAccessTokenService accessTokenService) : IAuthenticateService
    {
        private readonly IAccessTokenService _accessTokenService = accessTokenService;

        public Task<string> Authenticate(UserDto user)
        {
            var token = _accessTokenService.Generate(user);
            return Task.FromResult(token);
        }
    }
}
