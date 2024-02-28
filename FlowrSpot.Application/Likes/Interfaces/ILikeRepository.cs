using FlowrSpot.Application.Common.Interfaces;
using FlowrSpot.Domain.Entities;

namespace FlowrSpot.Application.Likes.Interfaces
{
    public interface ILikeRepository : IRepository<Like>, IReadRepository<Like> { }
}
