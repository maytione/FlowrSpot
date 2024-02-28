using AutoMapper;
using FlowrSpot.Application.Flowers.Command;
using FlowrSpot.Application.Flowers.Dtos;
using FlowrSpot.Domain.Entities;


namespace FlowrSpot.Application.Flowers.MappingProfiles
{
    public class FlowerProfiles:Profile
    {
        public FlowerProfiles()
        {
            CreateMap<CreateFlowerCommand, FlowerDto>().ReverseMap();
            CreateMap<CreateFlowerCommand, FlowerCreateDto>().ReverseMap();
            CreateMap<CreateFlowerCommand, Flower>().ReverseMap();

            CreateMap<UpdateFlowerCommand, FlowerDto>().ReverseMap();
            CreateMap<UpdateFlowerCommand, FlowerCreateDto>().ReverseMap();
            CreateMap<UpdateFlowerCommand, Flower>().ReverseMap();

            CreateMap<FlowerDto, Flower>().ReverseMap();

        }
    }
}
