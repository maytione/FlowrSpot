using AutoMapper;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Command;
using FlowrSpot.Application.Flowers.Dtos;
using FlowrSpot.Application.Flowers.Query;
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
    public class FlowerController(IMapper mapper, IMediator mediator) : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFlowers([FromQuery] PaginatedRequest paginatedRequest, CancellationToken cancellationToken)
        {
            var command = new GetAllFlowersQuery() { PageNumber = paginatedRequest.PageNumber, PageSize = paginatedRequest.PageSize };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> CreateFlower([FromBody] FlowerCreateDto flower, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return HandleModelStateError(ModelState);
            var command = _mapper.Map<CreateFlowerCommand>(flower);
            command.UserId = HttpContext.GetIdentityIdClaimValue();
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetFlower(int id, CancellationToken cancellationToken)
        {
            var query = new GetFlowerByIdQuery() { Id = id, UserId = HttpContext.GetIdentityIdClaimValue() };
            var result = await _mediator.Send(query, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }


        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> UpdateFlower(int id, [FromBody] FlowerDto flower, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return HandleModelStateError(ModelState);
            if (id != flower.Id) return HandleErrorResponseBadId();
            var command = _mapper.Map<UpdateFlowerCommand>(flower);
            command.UserId = HttpContext.GetIdentityIdClaimValue();
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFlower(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteFlowerCommand() { Id = id, UserId = HttpContext.GetIdentityIdClaimValue()};
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(result.Data);
        }

    }
}
