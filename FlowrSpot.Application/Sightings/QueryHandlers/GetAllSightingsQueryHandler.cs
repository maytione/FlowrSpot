using AutoMapper;
using FlowrSpot.Application.Common.Interfaces;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Sightings.Dtos;
using FlowrSpot.Application.Sightings.Interfaces;
using FlowrSpot.Application.Sightings.Query;
using MediatR;


namespace FlowrSpot.Application.Sightings.QueryHandlers
{
    internal class GetAllSightingsQueryHandler(IMapper mapper, ISightingRepository sightingRepository) : IRequestHandler<GetAllSightingsQuery, OperationResult<IPaginatedList<SightingDto>>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ISightingRepository _sightingRepository = sightingRepository;

        public async Task<OperationResult<IPaginatedList<SightingDto>>> Handle(GetAllSightingsQuery request, CancellationToken cancellationToken)
        {
            var (Result, TotalCount) = await _sightingRepository.GetPagedWithLikesAsync(request.PageNumber, request.PageSize,null, q => q.OrderBy(e => e.Id));
            var mapped = _mapper.ProjectTo<SightingDto>(Result.AsQueryable()).ToList().AsReadOnly();
            PaginatedList<SightingDto> paginated = new(mapped, TotalCount, request.PageNumber, request.PageSize);
            return new OperationResult<IPaginatedList<SightingDto>>() { Data = paginated };
        }
    }
}
