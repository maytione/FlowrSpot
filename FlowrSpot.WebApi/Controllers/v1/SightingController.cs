using AutoMapper;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Sightings.Command;
using FlowrSpot.Application.Sightings.Dtos;
using FlowrSpot.Application.Sightings.Query;
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
    public class SightingController(IMapper mapper, IMediator mediator) : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSightings([FromQuery] PaginatedRequest paginatedRequest, CancellationToken cancellationToken)
        {
            var command = new GetAllSightingsQuery() { PageNumber = paginatedRequest.PageNumber, PageSize = paginatedRequest.PageSize };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> CreateSighting([FromBody] SightingCreateDto sighting, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return HandleModelStateError(ModelState);
            var command = _mapper.Map<CreateSightingCommand>(sighting);
            command.UserId = HttpContext.GetIdentityIdClaimValue();
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSighting(int id, CancellationToken cancellationToken)
        {
            var query = new GetSightingByIdQuery() { Id = id, UserId = HttpContext.GetIdentityIdClaimValue() };
            var result = await _mediator.Send(query, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }


        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> UpdateSighting(int id, [FromBody] SightingUpdateDto sighting, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return HandleModelStateError(ModelState);
            if (id != sighting.Id) return HandleErrorResponseBadId();
            var command = _mapper.Map<UpdateSightingCommand>(sighting);
            command.UserId = HttpContext.GetIdentityIdClaimValue();
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSighting(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteSightingCommand() { Id = id, UserId = HttpContext.GetIdentityIdClaimValue() };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }
    }
}
