using AutoMapper;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Dtos;
using FlowrSpot.Application.Flowers.Interfaces;
using FlowrSpot.Application.Flowers.Query;
using FlowrSpot.Domain.Entities;
using MediatR;

namespace FlowrSpot.Application.Flowers.QueryHandlers
{
    internal class GetFlowerByIdQueryHandler(IMapper mapper, IFlowerRepository flowerRepository) : IRequestHandler<GetFlowerByIdQuery, OperationResult<FlowerDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IFlowerRepository _flowerRepository = flowerRepository;

        public async Task<OperationResult<FlowerDto>> Handle(GetFlowerByIdQuery request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<FlowerDto>();
            var flower = await _flowerRepository.GetBySpecAsync<Flower>(w =>
                  w.Id == request.Id &&
                  w.UserId.ToLower() ==request.UserId.ToLower(),cancellationToken);
            if (flower==null)
            {
                _result.AddError(Common.Enums.ErrorCode.NotFound, $"Flower with ID {request.Id} not found");
                return _result;
            }
            _result.Data = _mapper.Map<FlowerDto>(flower);
            return _result;
        }
    }
}
