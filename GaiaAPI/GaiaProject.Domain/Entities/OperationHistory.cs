namespace GaiaProject.Domain.Entities;

// A34D - Operation History Entity
/// <summary>
/// Stores history of all executed operations with their inputs and results
/// Used for analytics and displaying recent operations
/// </summary>
public class OperationHistory : BaseEntity
{
    public int OperationId { get; set; }
    public string FieldA { get; set; } = string.Empty;
    public string FieldB { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
    public string? ClientInfo { get; set; } // Optional: IP, User Agent, etc.
    
    // Navigation property
    public virtual Operation Operation { get; set; } = null!;
}
