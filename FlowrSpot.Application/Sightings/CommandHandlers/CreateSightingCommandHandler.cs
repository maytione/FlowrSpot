using AutoMapper;
using FlowrSpot.Application.Common.Models;
using FlowrSpot.Application.Flowers.Interfaces;
using FlowrSpot.Application.QuoteOfTheDay.Dtos;
using FlowrSpot.Application.QuoteOfTheDay.Service;
using FlowrSpot.Application.Sightings.Command;
using FlowrSpot.Application.Sightings.Dtos;
using FlowrSpot.Application.Sightings.Interfaces;
using FlowrSpot.Domain.Entities;
using MediatR;

namespace FlowrSpot.Application.Sightings.CommandHandlers
{
    internal class CreateSightingCommandHandler(
        IMapper mapper, 
        ISightingRepository sightingRepository, 
        IFlowerRepository flowerRepository, 
        QuoteOfTheDayService quoteOfTheDayService) : IRequestHandler<CreateSightingCommand, OperationResult<SightingDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ISightingRepository _sightingRepository = sightingRepository;
        private readonly IFlowerRepository _flowerRepository = flowerRepository;
        private readonly QuoteOfTheDayService _quoteOfTheDayService = quoteOfTheDayService;

        public async Task<OperationResult<SightingDto>> Handle(CreateSightingCommand request, CancellationToken cancellationToken)
        {
            var _result = new OperationResult<SightingDto>();
            try
            {
                var flower = await _flowerRepository.GetByIdAsync(request.FlowerId, cancellationToken);
                if (flower == null)
                {
                    _result.AddError(Common.Enums.ErrorCode.NotFound, $"Flower with ID {request.FlowerId} not found");
                    return _result;
                }
                var sighting = _mapper.Map<Sighting>(request);
                var existing = await _sightingRepository.GetBySpecAsync<Sighting>(w => 
                    w.Longitude.Equals(request.Longitude) &&
                    w.Latitude.Equals(request.Latitude) &&
                    w.FlowerId.Equals(request.FlowerId),cancellationToken);

                if (existing != null)
                {
                    _result.AddError(Common.Enums.ErrorCode.DuplicateEntry, $"Sighting exists with flower id {request.FlowerId}");
                    return _result;
                }

                var qod = await _quoteOfTheDayService.GetQuoteOfTheDay();
                if (qod != null && qod.Contents!=null && qod.Contents.Quotes!=null)
                {
                    QuoteDto? firstQuote = qod.Contents.Quotes.FirstOrDefault();
                    if (firstQuote != null)
                    {
                        sighting.QuoteOfTheDay = firstQuote.Quote + " - " + firstQuote.Author;
                    }
                }

                await _sightingRepository.AddAsync(sighting, cancellationToken);
                var result = await _sightingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                _result.Data = _mapper.Map<SightingDto>(sighting);
            }
            catch (Exception ex)
            {
                _result.AddUnknownError(ex.Message);
            }
            return _result;
        }
    }
}
