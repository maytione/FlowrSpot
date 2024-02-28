using System.Data;

namespace FlowrSpot.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction BeginTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
