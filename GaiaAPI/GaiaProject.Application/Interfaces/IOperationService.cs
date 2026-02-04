using GaiaProject.Application.DTOs;

namespace GaiaProject.Application.Interfaces;

// A34D - Operation Service Interface
/// <summary>
/// Service interface for operation management and execution
/// Defines business logic layer operations
/// </summary>
public interface IOperationService
{
    Task<IEnumerable<OperationDto>> GetAllOperationsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<OperationDto>> GetActiveOperationsAsync(CancellationToken cancellationToken = default);
    Task<OperationDto?> GetOperationByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OperationExecuteResponseDto> ExecuteOperationAsync(OperationExecuteRequestDto request, CancellationToken cancellationToken = default);
    Task<OperationDto> CreateOperationAsync(CreateOperationDto dto, CancellationToken cancellationToken = default);
    Task<OperationDto> UpdateOperationAsync(int id, CreateOperationDto dto, CancellationToken cancellationToken = default);
    Task<bool> ChangeStatusAsync(int id, ChangeStatusDto dto, CancellationToken cancellationToken = default);
}
