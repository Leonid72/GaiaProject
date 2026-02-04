namespace GaiaProject.Application.DTOs;

// A34D - Data Transfer Objects

/// <summary>
/// DTO for listing available operations
/// </summary>
public class OperationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OperationType { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
}

/// <summary>
/// Request DTO for executing an operation
/// </summary>
public class OperationExecuteRequestDto
{
    public string OperationName { get; set; } = string.Empty;
    public string FieldA { get; set; } = string.Empty;
    public string FieldB { get; set; } = string.Empty;
}

/// <summary>
/// Response DTO for operation execution result
/// </summary>
public class OperationExecuteResponseDto
{
    public bool IsSuccess { get; set; }
    public string Result { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
    public OperationHistoryInfoDto? HistoryInfo { get; set; }
}

/// <summary>
/// DTO for operation history information
/// </summary>
public class OperationHistoryInfoDto
{
    public List<OperationHistoryItemDto> LastThreeOperations { get; set; } = new();
    public int MonthlyOperationCount { get; set; }
}

/// <summary>
/// DTO for individual history item
/// </summary>
public class OperationHistoryItemDto
{
    public string FieldA { get; set; } = string.Empty;
    public string FieldB { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public DateTime ExecutedAt { get; set; }
}

/// <summary>
/// Request DTO for creating/updating operations
/// </summary>
public class CreateOperationDto
{
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OperationType { get; set; } = string.Empty;
    public string ImplementationClass { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

/// <summary>
/// Request to change status
/// </summary>
public sealed record ChangeStatusDto(bool IsActive);