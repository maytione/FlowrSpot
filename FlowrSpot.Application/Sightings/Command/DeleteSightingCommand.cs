using FlowrSpot.Application.Common.Models;
using MediatR;

namespace FlowrSpot.Application.Sightings.Command
{
    public class DeleteSightingCommand: IRequest<OperationResult<bool>>
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
    }
}
