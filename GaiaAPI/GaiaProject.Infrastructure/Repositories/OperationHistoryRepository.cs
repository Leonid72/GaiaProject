using GaiaProject.Application.Interfaces;
using GaiaProject.Domain.Entities;
using GaiaProject.Domain.Interfaces;
using GaiaProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GaiaProject.Infrastructure.Repositories;

// A34D - Operation History Repository Implementation
/// <summary>
/// Specific repository for OperationHistory entity
/// Provides queries for historical operation data
/// </summary>
public class OperationHistoryRepository : GenericRepository<OperationHistory>, IOperationHistoryRepository
{
    public OperationHistoryRepository(IGaiaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OperationHistory>> GetLastNOperationsAsync(
        int operationId, 
        int count, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(h => h.OperationId == operationId && h.IsSuccess)
            .OrderByDescending(h => h.ExecutedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetMonthlyOperationCountAsync(
        int operationId, 
        int year, 
        int month, 
        CancellationToken cancellationToken = default)
    {
        var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
        var endDate = startDate.AddMonths(1);

        return await _dbSet
            .Where(h => h.OperationId == operationId 
                     && h.ExecutedAt >= startDate 
                     && h.ExecutedAt < endDate)
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<OperationHistory>> GetOperationsByDateRangeAsync(
        DateTime startDate, 
        DateTime endDate, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(h => h.Operation)
            .Where(h => h.ExecutedAt >= startDate 
                     && h.ExecutedAt <= endDate)
            .OrderByDescending(h => h.ExecutedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<OperationHistory>> GetSuccessfulOperationsAsync(
        int operationId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(h => h.OperationId == operationId && h.IsSuccess)
            .OrderByDescending(h => h.ExecutedAt)
            .ToListAsync(cancellationToken);
    }
}
