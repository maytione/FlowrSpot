using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Dtos;
using MediatR;

namespace FlowrSpot.Application.Flowers.Command
{
    public class CreateFlowerCommand: FlowerCreateDto , IRequest<OperationResult<FlowerDto>>
    {
        public required string UserId { get; set; }
    }
}
