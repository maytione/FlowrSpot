using AutoMapper;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Sightings.Dtos;
using FlowrSpot.Application.Sightings.Interfaces;
using FlowrSpot.Application.Sightings.Query;
using MediatR;

namespace FlowrSpot.Application.Sightings.QueryHandlers
{
    internal class GetSightingByIdQueryHandler(IMapper mapper, ISightingRepository sightingRepository) : IRequestHandler<GetSightingByIdQuery, OperationResult<SightingDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ISightingRepository _sightingRepository = sightingRepository;

        public async Task<OperationResult<SightingDto>> Handle(GetSightingByIdQuery request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<SightingDto>();
            var sighting = await _sightingRepository.GetBySpecWithLikesAndFlowerAsync(w =>
                  w.Id == request.Id &&
                  w.UserId.ToLower() == request.UserId.ToLower());
            if (sighting== null)
            {
                _result.AddError(Common.Enums.ErrorCode.NotFound, $"Sighting with ID {request.Id} not found");
                return _result;
            }
            _result.Data = _mapper.Map<SightingDto>(sighting);
            return _result;
        }
    }
}
