using FlowrSpot.Application.Likes.Interfaces;
using FlowrSpot.Domain.Entities;


namespace FlowrSpot.Infrastructure.Data.Repository
{
    internal class LikeRepository(ApplicationDbContext context) : BaseRepository<Like>(context), ILikeRepository
    {

    }
}
