using FlowrSpot.Application.Common.Interfaces;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Dtos;
using MediatR;

namespace FlowrSpot.Application.Flowers.Query
{
    public class GetAllFlowersQuery: PaginatedRequest, IRequest<OperationResult<IPaginatedList<FlowerDto>>>
    {
    }
}
