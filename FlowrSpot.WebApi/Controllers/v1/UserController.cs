using AutoMapper;
using FlowrSpot.Application.Users.Command;
using FlowrSpot.Application.Users.Dtos;
using FlowrSpot.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowrSpot.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController(IMapper mapper, IMediator mediator) : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Route("login")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto login, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<LoginCommand>(login);
            if (command == null) return BadRequest();
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Data);
        }

        [HttpPost]
        [Route("register")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto register, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterCommand>(register);
            if (command==null) return BadRequest();
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Data);
        }

    }
}
