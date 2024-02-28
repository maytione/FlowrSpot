using FlowrSpot.Application.Common.Interfaces;
using FlowrSpot.Domain.Entities;
using FlowrSpot.Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlowrSpot.Infrastructure.Interfaces
{
    public interface IApplicationDbContext: IUnitOfWork
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; }
        public DbSet<Flower> Flowers { get; }
        public DbSet<Sighting> Sightings { get; }
        public DbSet<Like> Likes { get; }
    }
}
