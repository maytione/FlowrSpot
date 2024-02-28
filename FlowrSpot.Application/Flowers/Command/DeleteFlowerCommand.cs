using FlowrSpot.Application.Common.Models;
using MediatR;

namespace FlowrSpot.Application.Flowers.Command
{
    public class DeleteFlowerCommand: IRequest<OperationResult<bool>>
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
    }
}
