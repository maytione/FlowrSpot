using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Likes.Command;
using FlowrSpot.Application.Likes.Interfaces;
using FlowrSpot.Application.Sightings.Interfaces;
using FlowrSpot.Domain.Entities;
using MediatR;


namespace FlowrSpot.Application.Likes.CommandHandlers
{
    internal class DeleteLikeCommandHandler(ILikeRepository likeRepository, ISightingRepository sightingRepository) : IRequestHandler<DeleteLikeCommand, OperationResult<bool>>
    {
        private readonly ILikeRepository _likeRepository = likeRepository;
        private readonly ISightingRepository _sightingRepository = sightingRepository;

        public async Task<OperationResult<bool>> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<bool>();
            try
            {
                var sighting = await _sightingRepository.GetByIdAsync(request.SightingId, cancellationToken);
                if (sighting == null)
                {
                    _result.AddError(Common.Enums.ErrorCode.NotFound, $"Sighting with ID {request.SightingId} not found");
                    return _result;
                }

                var like = await _likeRepository.GetBySpecAsync<Like>(l => l.UserId == request.UserId && l.SightingId == request.SightingId,cancellationToken);
                if (like==null)
                {
                    _result.AddError(Common.Enums.ErrorCode.NotFound, "You have not liked this sighting.");
                    return _result;
                }
               
                await _likeRepository.DeleteAsync(like, cancellationToken);
                var result = await _likeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                _result.Data = result > 0;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError(ex.Message);
            }
            return _result;
        }
    }
}
