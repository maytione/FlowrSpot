using AutoMapper;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Users.Command;
using FlowrSpot.Application.Users.Dtos;
using FlowrSpot.Application.Users.Interfaces;
using MediatR;

namespace FlowrSpot.Application.Users.CommandHandlers
{
    internal class LoginCommandHandler(IIdentityRepository identityRepository, IMapper mapper) : IRequestHandler<LoginCommand, OperationResult<AuthResponseDto>>
    {
        private readonly IIdentityRepository _identityRepository = identityRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<AuthResponseDto>();

            try
            {
                // lets try find user by email
                var userDto = await _identityRepository.FindByEmailAsync(request.Email);

                // no user found, return some unclear message
                if (userDto == null)
                {
                    result.AddError(Common.Enums.ErrorCode.AuthError, "Check username and/or password");
                    return result;
                }

                // user found, lets try to sign in
                var signedIn = await _identityRepository.PasswordSignInAsync(userDto, request.Password);

                // password missmatched, return some unclear message
                if (!signedIn)
                {
                    result.AddError(Common.Enums.ErrorCode.AuthError, "Check username and/or password");
                    return result;
                }

                // all good, generate some JWT token
                var token = await _identityRepository.AuthenticateAsync(userDto);

                userDto.AccessToken = token;

                result.Data = _mapper.Map<AuthResponseDto>(userDto);
            }
            catch (Exception)
            {
                result.AddError(Common.Enums.ErrorCode.AuthError, "Check username and/or password");
            }

            return result;

        }
    }
}
