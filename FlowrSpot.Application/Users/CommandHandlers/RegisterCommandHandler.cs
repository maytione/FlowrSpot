using AutoMapper;
using FlowrSpot.Application.Common.Enums;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Users.Command;
using FlowrSpot.Application.Users.Dtos;
using FlowrSpot.Application.Users.Interfaces;
using MediatR;


namespace FlowrSpot.Application.Users.CommandHandlers
{
    internal class RegisterCommandHandler(IIdentityRepository identityRepository, IMapper mapper) : IRequestHandler<RegisterCommand, OperationResult<AuthResponseDto>>
    {
        private readonly IIdentityRepository _identityRepository = identityRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<AuthResponseDto>();
            var user = _mapper.Map<UserDto>(request);
            var identityUser = await _identityRepository.CreateUserAsync(user, request.Password );
            if (identityUser.IsError)
            {
                foreach (var identityError in identityUser.Errors)
                {
                    _result.AddError(ErrorCode.CreateError, identityError.Message);
                }
                return _result;
            }
            var authResult = await _identityRepository.AuthenticateAsync(user);
            _result.Data = _mapper.Map<AuthResponseDto>(user);
            _result.Data.AccessToken = authResult;
            return _result;
        }
    }
}
