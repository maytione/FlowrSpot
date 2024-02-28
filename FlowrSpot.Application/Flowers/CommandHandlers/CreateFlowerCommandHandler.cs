using AutoMapper;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Command;
using FlowrSpot.Application.Flowers.Dtos;
using FlowrSpot.Application.Flowers.Interfaces;
using FlowrSpot.Domain.Entities;
using MediatR;

namespace FlowrSpot.Application.Flowers.CommandHandlers
{
    internal class CreateFlowerCommandHandler(IMapper mapper, IFlowerRepository flowerRepository) : IRequestHandler<CreateFlowerCommand, OperationResult<FlowerDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IFlowerRepository _flowerRepository = flowerRepository;

        public async Task<OperationResult<FlowerDto>> Handle(CreateFlowerCommand request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<FlowerDto>();
            try
            {
                var flower = _mapper.Map<Flower>(request);
                var existing = await _flowerRepository.GetBySpecAsync<Flower>(w => 
                    w.Name.ToLower() == request.Name.ToLower() && 
                    w.UserId.ToLower() == request.UserId.ToLower(),cancellationToken);

                if (existing!=null)
                {
                    _result.AddError(Common.Enums.ErrorCode.DuplicateEntry, $"Flower exists with name {request.Name}");
                    return _result;
                }
                await _flowerRepository.AddAsync(flower, cancellationToken);
                var result = await _flowerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                _result.Data = _mapper.Map<FlowerDto>(flower);
            }
            catch (Exception ex)
            {
                _result.AddUnknownError(ex.Message);
            }
            return _result;
        }
    }
}
