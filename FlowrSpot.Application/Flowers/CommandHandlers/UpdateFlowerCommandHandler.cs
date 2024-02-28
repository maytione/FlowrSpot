
using AutoMapper;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Command;
using FlowrSpot.Application.Flowers.Dtos;
using FlowrSpot.Application.Flowers.Interfaces;
using FlowrSpot.Domain.Entities;
using MediatR;

namespace FlowrSpot.Application.Flowers.CommandHandlers
{
    internal class UpdateFlowerCommandHandler(IMapper mapper, IFlowerRepository flowerRepository) : IRequestHandler<UpdateFlowerCommand, OperationResult<FlowerDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IFlowerRepository _flowerRepository = flowerRepository;

        public async Task<OperationResult<FlowerDto>> Handle(UpdateFlowerCommand request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<FlowerDto>();
            var flower = await _flowerRepository.GetBySpecAsync<Flower>(w =>
                  w.Id == request.Id &&
                  w.UserId.ToLower() == request.UserId.ToLower(), cancellationToken);
            if (flower == null)
            {
                _result.AddError(Common.Enums.ErrorCode.NotFound, $"Flower with ID {request.Id} not found");
                return _result;
            }
            flower = _mapper.Map(request, flower);
            await _flowerRepository.UpdateAsync(flower, cancellationToken);
            _result.Data = _mapper.Map<FlowerDto>(flower);
            return _result;
        }
    }
}
