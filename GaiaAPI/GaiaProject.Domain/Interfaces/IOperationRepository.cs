using GaiaProject.Domain.Entities;

namespace GaiaProject.Domain.Interfaces;

// A34D - Operation Repository Interface
/// <summary>
/// Specific repository interface for Operation entity
/// Extends generic repository with operation-specific queries
/// </summary>
public interface IOperationRepository : IGenericRepository<Operation>
{
    Task<IEnumerable<Operation>> GetActiveOperationsAsync(CancellationToken cancellationToken = default);
    Task<Operation?> GetOperationByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Operation>> GetOperationsByTypeAsync(string operationType, CancellationToken cancellationToken = default);
    Task<bool> ChangeStatusAsync(int id, bool isActive, CancellationToken cancellationToken = default);
}
