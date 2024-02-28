using FlowrSpot.Application.Common.Interfaces;
using FlowrSpot.Domain.Entities;

namespace FlowrSpot.Application.Flowers.Interfaces
{
    public interface IFlowerRepository : IRepository<Flower>, IReadRepository<Flower> { }
}
