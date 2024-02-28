using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowrSpot.Application.Common.Interfaces
{
    public interface IRepositoryBase<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
