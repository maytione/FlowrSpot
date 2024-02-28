using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Sightings.Dtos;
using MediatR;

namespace FlowrSpot.Application.Sightings.Command
{
    public class CreateSightingCommand: SightingCreateDto, IRequest<OperationResult<SightingDto>>
    {
        public required string UserId { get; set; }
    }
}
