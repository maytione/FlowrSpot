using FlowrSpot.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace FlowrSpot.Infrastructure.Data.Repository
{
    public class BaseRepository<TEntity>(ApplicationDbContext context) : IRepository<TEntity>, IReadRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public virtual async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity?> GetBySpecAsync<Spec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Remove(entity);

            return await _context.SaveChangesAsync(cancellationToken);
        }
              
        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Update(entity);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<(IEnumerable<TEntity> Result, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var count = await query.CountAsync();

            return (await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(), count);
        }

    }
}
