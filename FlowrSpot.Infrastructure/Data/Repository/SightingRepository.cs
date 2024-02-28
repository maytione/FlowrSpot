using FlowrSpot.Application.Sightings.Interfaces;
using FlowrSpot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace FlowrSpot.Infrastructure.Data.Repository
{
    internal class SightingRepository(ApplicationDbContext context) : BaseRepository<Sighting>(context), ISightingRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<(IEnumerable<Sighting> Result, int TotalCount)> GetPagedWithLikesAsync(int pageNumber, int pageSize, Expression<Func<Sighting, bool>>? filter = null, Func<IQueryable<Sighting>, IOrderedQueryable<Sighting>>? orderBy = null)
        {
            IQueryable<Sighting> query = _context.Set<Sighting>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            query.AsNoTracking();
            var count = await query.CountAsync();
            return (await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Include(s=>s.Likes).Include(f=>f.Flower).ToListAsync(), count);
        }

        public async Task<Sighting?> GetBySpecWithLikesAndFlowerAsync(Expression<Func<Sighting, bool>> predicate, CancellationToken cancellationToken = default)
        {
             return await _context.Set<Sighting>().Include(s => s.Likes).Include(f => f.Flower).FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
