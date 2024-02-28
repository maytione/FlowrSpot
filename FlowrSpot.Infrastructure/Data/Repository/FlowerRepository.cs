using FlowrSpot.Application.Flowers.Interfaces;
using FlowrSpot.Domain.Entities;


namespace FlowrSpot.Infrastructure.Data.Repository
{
    internal class FlowerRepository(ApplicationDbContext context) : BaseRepository<Flower>(context), IFlowerRepository
    {
    }
}
