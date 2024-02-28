using AutoMapper;
using FlowrSpot.Application.Common.Interfaces;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Dtos;
using FlowrSpot.Application.Flowers.Interfaces;
using FlowrSpot.Application.Flowers.Query;
using FlowrSpot.Domain.Entities;
using MediatR;


namespace FlowrSpot.Application.Flowers.QueryHandlers
{
    internal class GetAllFlowersQueryHandler(IMapper mapper, IFlowerRepository flowerRepository) : IRequestHandler<GetAllFlowersQuery, OperationResult<IPaginatedList<FlowerDto>>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IFlowerRepository _flowerRepository = flowerRepository;

        public async Task<OperationResult<IPaginatedList<FlowerDto>>> Handle(GetAllFlowersQuery request, CancellationToken cancellationToken)
        {
            var (Result, TotalCount) = await _flowerRepository.GetPagedAsync(request.PageNumber, request.PageSize,null, q => q.OrderBy(e => e.Id));
            var mapped = _mapper.ProjectTo<FlowerDto>(Result.AsQueryable()).ToList().AsReadOnly();
            PaginatedList<FlowerDto> paginated = new(mapped, TotalCount, request.PageNumber, request.PageSize);
            return new OperationResult<IPaginatedList<FlowerDto>>() { Data = paginated };
        }
    }
}
