using AutoMapper;
using FlowrSpot.Application.Likes.Command;
using FlowrSpot.WebApi.Extensions;
using FlowrSpot.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowrSpot.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LikeController(IMapper mapper, IMediator mediator) : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        [HttpPost("{sightingId}")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> LikeSighting(int sightingId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return HandleModelStateError(ModelState);
            var command = new CreateLikeCommand() { SightingId = sightingId, UserId = HttpContext.GetIdentityIdClaimValue() };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }

        [HttpDelete("{sightingId}")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> UnlikeSighting(int sightingId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return HandleModelStateError(ModelState);
            var command = new DeleteLikeCommand() { SightingId = sightingId, UserId = HttpContext.GetIdentityIdClaimValue() };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }
    }
}
