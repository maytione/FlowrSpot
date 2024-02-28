using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Sightings.Command;
using FlowrSpot.Application.Sightings.Interfaces;
using FlowrSpot.Domain.Entities;
using MediatR;


namespace FlowrSpot.Application.Sightings.CommandHandlers
{
    internal class DeleteSightingCommandHandler(ISightingRepository sightingRepository) : IRequestHandler<DeleteSightingCommand, OperationResult<bool>>
    {
        private readonly ISightingRepository _sightingRepository = sightingRepository;

        public async Task<OperationResult<bool>> Handle(DeleteSightingCommand request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<bool>();
            var sighting = await _sightingRepository.GetBySpecAsync<Sighting>(w =>
                    w.Id == request.Id &&
                    w.UserId.Equals(request.UserId), cancellationToken);
            if (sighting == null)
            {
                _result.AddError(Common.Enums.ErrorCode.NotFound, $"Sighting with ID {request.Id} not found");
                return _result;
            }
            _result.Data = await _sightingRepository.DeleteAsync(sighting, cancellationToken) > 0;
            return _result;
        }
    }
}
