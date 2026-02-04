using GaiaProject.Application.Interfaces;
using GaiaProject.Domain.Entities;
using GaiaProject.Domain.Interfaces;
using GaiaProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GaiaProject.Infrastructure.Repositories;

// A34D - Operation Repository Implementation
/// <summary>
/// Specific repository for Operation entity
/// Extends generic repository with operation-specific queries
/// </summary>
public class OperationRepository : GenericRepository<Operation>, IOperationRepository
{
    public OperationRepository(IGaiaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Operation>> GetActiveOperationsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.IsActive)
            .OrderBy(o => o.SortOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<Operation?> GetOperationByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(o => o.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Operation>> GetOperationsByTypeAsync(
        string operationType, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.OperationType == operationType)
            .OrderBy(o => o.SortOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ChangeStatusAsync(int id, bool isActive, CancellationToken ct)
    {
        var entity = await GetByIdAsync(id, ct);
        if (entity is null)
            return false;

        entity.IsActive = isActive;
        entity.UpdatedAt = DateTime.UtcNow;
        return true; // EF tracks entity, Update() not required
    }
}
