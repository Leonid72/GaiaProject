using GaiaProject.Application.DTOs;
using GaiaProject.Application.Interfaces;
using GaiaProject.Application.Options;
using GaiaProject.Domain.Entities;
using GaiaProject.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace GaiaProject.Application.Services;

// A34D - Operation Service Implementation
/// <summary>
/// Main service for managing and executing operations
/// Implements business logic and coordinates between repositories and executors
/// </summary>
public class OperationService : IOperationService
{
    private readonly IOperationRepository _operationRepo;
    private readonly IOperationHistoryRepository _historyRepo;
    private readonly IGaiaDbContext _context; 
    private readonly IEnumerable<IOperationExecutor> _operationExecutors;
    private readonly OperationHistoryOptions _historyOptions;

    public OperationService(
        IOperationRepository operationRepo,
        IOperationHistoryRepository historyRepo,
        IGaiaDbContext context,
        IEnumerable<IOperationExecutor> operationExecutors,
        IOptions<OperationHistoryOptions> historyOptions)
    {
        _operationRepo = operationRepo;
        _historyRepo = historyRepo;
        _context = context;
        _operationExecutors = operationExecutors;
        _historyOptions = historyOptions.Value;
    }

    public async Task<IEnumerable<OperationDto>> GetAllOperationsAsync(CancellationToken cancellationToken = default)
    {
        var operations = await _operationRepo.GetAllAsync(cancellationToken);
        return operations.Select(MapToDto);
    }

    public async Task<IEnumerable<OperationDto>> GetActiveOperationsAsync(CancellationToken cancellationToken = default)
    {
        var operations = await _operationRepo.GetActiveOperationsAsync(cancellationToken);
        return operations.Select(MapToDto);
    }

    public async Task<OperationDto?> GetOperationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var operation = await _operationRepo.GetByIdAsync(id, cancellationToken);
        return operation != null ? MapToDto(operation) : null;
    }

    public async Task<OperationExecuteResponseDto> ExecuteOperationAsync(
        OperationExecuteRequestDto request, 
        CancellationToken cancellationToken = default)
    {
        var response = new OperationExecuteResponseDto();
        
        try
        {
            var operation = await _operationRepo.GetOperationByNameAsync(request.OperationName, cancellationToken);
            
            if (operation == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"Operation '{request.OperationName}' not found";
                return response;
            }

            if (!operation.IsActive)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"Operation '{request.OperationName}' is not active";
                return response;
            }

            // Find the appropriate executor
            var executor = _operationExecutors.FirstOrDefault(e => 
                e.GetType().Name.Equals(operation.ImplementationClass, StringComparison.OrdinalIgnoreCase));

            if (executor == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"Executor for operation '{request.OperationName}' not found";
                return response;
            }

            // Execute the operation
            var result = executor.Execute(request.FieldA, request.FieldB);
            response.Result = result;
            response.IsSuccess = true;

            // Save to history
            var history = new OperationHistory
            {
                OperationId = operation.Id,
                FieldA = request.FieldA,
                FieldB = request.FieldB,
                Result = result,
                IsSuccess = true,
                ExecutedAt = DateTime.UtcNow
            };

            await _historyRepo.AddAsync(history, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Get history information (Bonus feature)
            response.HistoryInfo = await GetHistoryInfoAsync(operation.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = ex.Message;

            // Try to save failed operation to history
            try
            {
                var operation = await _operationRepo.GetOperationByNameAsync(request.OperationName, cancellationToken);
                if (operation != null)
                {
                    var history = new OperationHistory
                    {
                        OperationId = operation.Id,
                        FieldA = request.FieldA,
                        FieldB = request.FieldB,
                        Result = string.Empty,
                        IsSuccess = false,
                        ErrorMessage = ex.Message,
                        ExecutedAt = DateTime.UtcNow
                    };

                    await _historyRepo.AddAsync(history, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            catch
            {
                // Ignore errors when saving failed operation history
            }
        }

        return response;
    }

    public async Task<OperationDto> CreateOperationAsync(CreateOperationDto dto, CancellationToken cancellationToken = default)
    {
        var operation = new Operation
        {
            Name = dto.Name,
            DisplayName = dto.DisplayName,
            Description = dto.Description,
            OperationType = dto.OperationType,
            ImplementationClass = dto.ImplementationClass,
            IsActive = true,
            SortOrder = dto.SortOrder
        };

        var created = await _operationRepo.AddAsync(operation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(created);
    }

    public async Task<OperationDto> UpdateOperationAsync(int id, CreateOperationDto dto, CancellationToken cancellationToken = default)
    {
        var operation = await _operationRepo.GetByIdAsync(id, cancellationToken);
        
        if (operation == null)
            throw new KeyNotFoundException($"Operation with ID {id} not found");

        operation.Name = dto.Name;
        operation.DisplayName = dto.DisplayName;
        operation.Description = dto.Description;
        operation.OperationType = dto.OperationType;
        operation.ImplementationClass = dto.ImplementationClass;
        operation.SortOrder = dto.SortOrder;
        operation.UpdatedAt = DateTime.UtcNow;

        await _operationRepo.UpdateAsync(operation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(operation);
    }

    public async Task<bool> ChangeStatusAsync(int id, ChangeStatusDto dto, CancellationToken cancellationToken = default)
    {
        var result = await _operationRepo.ChangeStatusAsync(id, dto.IsActive, cancellationToken);
        if (result)
            await _context.SaveChangesAsync(cancellationToken);

        return result;
    }

    // Helper methods
    private static OperationDto MapToDto(Operation operation)
    {
        return new OperationDto
        {
            Id = operation.Id,
            Name = operation.Name,
            DisplayName = operation.DisplayName,
            Description = operation.Description,
            OperationType = operation.OperationType,
            IsActive = operation.IsActive,
            SortOrder = operation.SortOrder
        };
    }

    private async Task<OperationHistoryInfoDto> GetHistoryInfoAsync(int operationId, CancellationToken cancellationToken)
    {
        var historyInfo = new OperationHistoryInfoDto();

        // Get last 3 operations of same type
        var lastThree = await _historyRepo.GetLastNOperationsAsync(operationId, _historyOptions.LastCount, cancellationToken);
        historyInfo.LastThreeOperations = lastThree.Select(h => new OperationHistoryItemDto
        {
            FieldA = h.FieldA,
            FieldB = h.FieldB,
            Result = h.Result,
            ExecutedAt = h.ExecutedAt
        }).ToList();

        // Get monthly count
        var now = DateTime.UtcNow;
        historyInfo.MonthlyOperationCount = await _historyRepo
            .GetMonthlyOperationCountAsync(operationId, now.Year, now.Month, cancellationToken);

        return historyInfo;
    }
}
