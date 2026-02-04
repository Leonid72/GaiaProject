using GaiaProject.Domain.Entities;

namespace GaiaProject.Domain.Interfaces;

// A34D - Operation History Repository Interface
/// <summary>
/// Specific repository interface for OperationHistory entity
/// Provides methods for querying historical operation data
/// </summary>
public interface IOperationHistoryRepository : IGenericRepository<OperationHistory>
{
    Task<IEnumerable<OperationHistory>> GetLastNOperationsAsync(int operationId, int count, CancellationToken cancellationToken = default);
    Task<int> GetMonthlyOperationCountAsync(int operationId, int year, int month, CancellationToken cancellationToken = default);
    Task<IEnumerable<OperationHistory>> GetOperationsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<IEnumerable<OperationHistory>> GetSuccessfulOperationsAsync(int operationId, CancellationToken cancellationToken = default);
}
