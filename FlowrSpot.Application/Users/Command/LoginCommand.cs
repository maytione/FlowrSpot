using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Users.Dtos;
using MediatR;

namespace FlowrSpot.Application.Users.Command
{
    public class LoginCommand : IRequest<OperationResult<AuthResponseDto>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
