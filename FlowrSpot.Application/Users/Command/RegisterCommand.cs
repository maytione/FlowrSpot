using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Users.Dtos;
using MediatR;


namespace FlowrSpot.Application.Users.Command
{
    public class RegisterCommand: RegisterDto, IRequest<OperationResult<AuthResponseDto>>
    {
    }
}
