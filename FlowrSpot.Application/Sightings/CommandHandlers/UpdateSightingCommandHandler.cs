
using AutoMapper;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Interfaces;
using FlowrSpot.Application.Sightings.Command;
using FlowrSpot.Application.Sightings.Dtos;
using FlowrSpot.Application.Sightings.Interfaces;
using MediatR;

namespace FlowrSpot.Application.Sightings.CommandHandlers
{
    internal class UpdateSightingCommandHandler(IMapper mapper, ISightingRepository sightingRepository, IFlowerRepository flowerRepository) : IRequestHandler<UpdateSightingCommand, OperationResult<SightingDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ISightingRepository _sightingRepository = sightingRepository;
        private readonly IFlowerRepository _flowerRepository = flowerRepository;

        public async Task<OperationResult<SightingDto>> Handle(UpdateSightingCommand request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<SightingDto>();
            var flower = await _flowerRepository.GetByIdAsync(request.FlowerId, cancellationToken);
            if (flower == null)
            {
                _result.AddError(Common.Enums.ErrorCode.NotFound, $"Flower with ID {request.FlowerId} not found");
                return _result;
            }
            var sighting = await _sightingRepository.GetBySpecWithLikesAndFlowerAsync(w =>
                  w.Id == request.Id &&
                  w.UserId.Equals(request.UserId));
            if (sighting== null)
            {
                _result.AddError(Common.Enums.ErrorCode.NotFound, $"Sighting with ID {request.Id} not found");
                return _result;
            }
            sighting = _mapper.Map(request, sighting);
            await _sightingRepository.UpdateAsync(sighting, cancellationToken);
            _result.Data = _mapper.Map<SightingDto>(sighting);
            return _result;
        }
    }
}
