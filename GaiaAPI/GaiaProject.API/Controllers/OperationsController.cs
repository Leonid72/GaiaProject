using Microsoft.AspNetCore.Mvc;
using GaiaProject.Application.DTOs;
using GaiaProject.Application.Interfaces;

namespace GaiaProject.API.Controllers;

// A34D - Operations API Controller
/// <summary>
/// API Controller for managing and executing operations
/// Exposes RESTful endpoints for operation management
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OperationsController : ControllerBase
{
    private readonly IOperationService _operationService;
    private readonly ILogger<OperationsController> _logger;

    public OperationsController(IOperationService operationService, ILogger<OperationsController> logger)
    {
        _operationService = operationService;
        _logger = logger;
    }

    /// <summary>
    /// Get all available operations
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OperationDto>>> GetAllOperations(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all operations");
        var operations = await _operationService.GetAllOperationsAsync(cancellationToken);
        return Ok(operations);
    }

    /// <summary>
    /// Get all active operations
    /// </summary>
    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OperationDto>>> GetActiveOperations(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting active operations");
        var operations = await _operationService.GetActiveOperationsAsync(cancellationToken);
        return Ok(operations);
    }

    /// <summary>
    /// Get operation by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OperationDto>> GetOperationById(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting operation with ID: {Id}", id);
        var operation = await _operationService.GetOperationByIdAsync(id, cancellationToken);
        
        if (operation == null)
        {
            _logger.LogWarning("Operation with ID {Id} not found", id);
            return NotFound(new { message = $"Operation with ID {id} not found" });
        }

        return Ok(operation);
    }

    /// <summary>
    /// Execute an operation
    /// </summary>
    [HttpPost("execute")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OperationExecuteResponseDto>> ExecuteOperation(
        [FromBody] OperationExecuteRequestDto request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing operation: {OperationName} with FieldA: {FieldA}, FieldB: {FieldB}", 
            request.OperationName, request.FieldA, request.FieldB);

        var result = await _operationService.ExecuteOperationAsync(request, cancellationToken);

        if (!result.IsSuccess)
        {
            _logger.LogWarning("Operation execution failed: {ErrorMessage}", result.ErrorMessage);
            return BadRequest(result);
        }

        _logger.LogInformation("Operation executed successfully. Result: {Result}", result.Result);
        return Ok(result);
    }

    /// <summary>
    /// Create a new operation
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OperationDto>> CreateOperation(
        [FromBody] CreateOperationDto dto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new operation: {Name}", dto.Name);

        try
        {
            var operation = await _operationService.CreateOperationAsync(dto, cancellationToken);
            _logger.LogInformation("Operation created successfully with ID: {Id}", operation.Id);
            return CreatedAtAction(nameof(GetOperationById), new { id = operation.Id }, operation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating operation");
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing operation
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OperationDto>> UpdateOperation(
        int id,
        [FromBody] CreateOperationDto dto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating operation with ID: {Id}", id);

        try
        {
            var operation = await _operationService.UpdateOperationAsync(id, dto, cancellationToken);
            _logger.LogInformation("Operation updated successfully");
            return Ok(operation);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Operation not found");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating operation");
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// ChangeStatus an operation (sets IsActive = false)
    /// </summary>
    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeStatus(int id,[FromBody] ChangeStatusDto dto,
    CancellationToken ct)
    {
        _logger.LogInformation(
            "Changing status of operation {Id} to {IsActive}", id, dto.IsActive);

        var result = await _operationService.ChangeStatusAsync(id, dto, ct);

        if (!result)
            return NotFound(new { message = $"Operation with ID {id} not found" });

        return NoContent();
    }
}
