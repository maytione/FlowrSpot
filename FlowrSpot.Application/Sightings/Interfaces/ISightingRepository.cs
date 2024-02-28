using FlowrSpot.Application.Common.Interfaces;
using FlowrSpot.Domain.Entities;
using System.Linq.Expressions;

namespace FlowrSpot.Application.Sightings.Interfaces
{
    public interface ISightingRepository : IRepository<Sighting>, IReadRepository<Sighting> {
        Task<(IEnumerable<Sighting> Result, int TotalCount)> GetPagedWithLikesAsync(int pageNumber, int pageSize, Expression<Func<Sighting, bool>>? filter = null, Func<IQueryable<Sighting>, IOrderedQueryable<Sighting>>? orderBy = null);
        Task<Sighting?> GetBySpecWithLikesAndFlowerAsync(Expression<Func<Sighting, bool>> predicate, CancellationToken cancellationToken = default);

    }
}
