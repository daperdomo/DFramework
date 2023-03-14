using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DFramework.Application.Common.Interfaces
{
    public interface IDbContext
    {
        DatabaseFacade Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
