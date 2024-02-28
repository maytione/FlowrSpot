using FlowrSpot.Application.Common.Interfaces;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Sightings.Dtos;
using MediatR;

namespace FlowrSpot.Application.Sightings.Query
{
    public class GetAllSightingsQuery: PaginatedRequest, IRequest<OperationResult<IPaginatedList<SightingDto>>>
    {
    }
}
