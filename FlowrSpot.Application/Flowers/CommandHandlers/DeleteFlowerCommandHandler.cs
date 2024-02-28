using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Command;
using FlowrSpot.Application.Flowers.Interfaces;
using FlowrSpot.Domain.Entities;
using MediatR;


namespace FlowrSpot.Application.Flowers.CommandHandlers
{
    internal class DeleteFlowerCommandHandler(IFlowerRepository flowerRepository) : IRequestHandler<DeleteFlowerCommand, OperationResult<bool>>
    {
        private readonly IFlowerRepository _flowerRepository = flowerRepository;

        public async Task<OperationResult<bool>> Handle(DeleteFlowerCommand request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<bool>();
            var flower = await _flowerRepository.GetBySpecAsync<Flower>(w =>
                    w.Id == request.Id &&
                    w.UserId.ToLower() == request.UserId.ToLower(), cancellationToken);
            if (flower == null)
            {
                _result.AddError(Common.Enums.ErrorCode.NotFound, $"Flower with ID {request.Id} not found");
                return _result;
            }
            _result.Data = await _flowerRepository.DeleteAsync(flower, cancellationToken) > 0;
            return _result;
        }
    }
}
