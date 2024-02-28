using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Dtos;
using MediatR;

namespace FlowrSpot.Application.Flowers.Query
{
    public class GetFlowerByIdQuery: IRequest<OperationResult<FlowerDto>>
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
    }
}
