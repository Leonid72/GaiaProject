using GaiaProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaiaProject.Application.Interfaces
{
    public interface IGaiaDbContext 
    {
        DbSet<T> Set<T>() where T : class;
        DbSet<Operation> Operations { get; }
        DbSet<OperationHistory> OperationHistories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
