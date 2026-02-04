namespace GaiaProject.Domain.Entities;

// A34D - Operation Entity
/// <summary>
/// Represents an operation type that can be performed on two fields
/// Supports dynamic addition/removal of operations without code changes
/// </summary>
public class Operation : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OperationType { get; set; } = string.Empty; // Arithmetic, String, Logical, etc.
    public string ImplementationClass { get; set; } = string.Empty; // Full class name for reflection
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; }
    
    // Navigation property
    public virtual ICollection<OperationHistory> OperationHistories { get; set; } = new List<OperationHistory>();
}
