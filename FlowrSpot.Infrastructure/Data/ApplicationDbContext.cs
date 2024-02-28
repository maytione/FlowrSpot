using FlowrSpot.Domain.Entities;
using FlowrSpot.Infrastructure.Data.Identity;
using FlowrSpot.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;


namespace FlowrSpot.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
    {

        public DbSet<ApplicationUser> ApplicationUsers => base.Set<ApplicationUser>();
        public DbSet<Flower> Flowers => base.Set<Flower>();
        public DbSet<Sighting> Sightings => base.Set<Sighting>();
        public DbSet<Like> Likes => base.Set<Like>();

        public IDbTransaction BeginTransaction()
        {
            var trx = this.Database.BeginTransaction();
            return trx.GetDbTransaction();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sighting>()
                .HasOne(s => s.Flower)
                .WithMany(f => f.Sightings)
                .HasForeignKey(s => s.FlowerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                 .HasKey(l => new { l.UserId, l.SightingId });

            modelBuilder.Entity<Like>()
                 .HasOne(l => l.Sighting)
                 .WithMany(s => s.Likes)
                 .HasForeignKey(l => l.SightingId)
                 .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
