using System.Linq.Expressions;

namespace FlowrSpot.Application.Common.Interfaces
{
    public interface IReadRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
        Task<TEntity?> GetBySpecAsync<Spec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<(IEnumerable<TEntity> Result, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    }
}
