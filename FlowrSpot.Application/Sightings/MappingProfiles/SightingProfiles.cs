using AutoMapper;
using FlowrSpot.Application.Sightings.Command;
using FlowrSpot.Application.Sightings.Dtos;
using FlowrSpot.Domain.Entities;


namespace FlowrSpot.Application.Sightings.MappingProfiles
{
    public class SightingProfiles:Profile
    {
        public SightingProfiles()
        {
            CreateMap<CreateSightingCommand, SightingDto>().ReverseMap();
            CreateMap<CreateSightingCommand, SightingCreateDto>().ReverseMap();
            CreateMap<CreateSightingCommand, Sighting>().ReverseMap();

            CreateMap<UpdateSightingCommand, SightingDto>().ReverseMap();
            CreateMap<UpdateSightingCommand, SightingUpdateDto>().ReverseMap();


            CreateMap<SightingDto, Sighting>();

            CreateMap<Sighting, SightingDto>()
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.FlowerName, opt => opt.MapFrom(src => src.Flower.Name))
                .ForMember(dest => dest.FlowerImageRef, opt => opt.MapFrom(src => src.Flower.ImageRef));



            CreateMap<CreateSightingCommand, Sighting>()
                .ForMember(dest => dest.Likes, opt => opt.Ignore());
            CreateMap<UpdateSightingCommand, Sighting>()
                .ForMember(dest => dest.Likes, opt => opt.Ignore());
        }
    }
}
