using FlowrSpot.Application.Common.Models;
using MediatR;

namespace FlowrSpot.Application.Likes.Command
{
    public class CreateLikeCommand: IRequest<OperationResult<bool>>
    {
        public required string UserId { get; set; }
        public required int SightingId { get; set; }
    }
}
