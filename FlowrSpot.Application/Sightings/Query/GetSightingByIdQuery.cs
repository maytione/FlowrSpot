using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Sightings.Dtos;
using MediatR;

namespace FlowrSpot.Application.Sightings.Query
{
    public class GetSightingByIdQuery: IRequest<OperationResult<SightingDto>>
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
    }
}
