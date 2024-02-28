using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Sightings.Dtos;
using MediatR;

namespace FlowrSpot.Application.Sightings.Command
{
    public class UpdateSightingCommand: SightingUpdateDto, IRequest<OperationResult<SightingDto>>
    {
        public required string UserId { get; set; }
    }
}
