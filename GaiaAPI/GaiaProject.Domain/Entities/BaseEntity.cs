namespace GaiaProject.Domain.Entities;

// A34D - Base Entity for all domain entities
/// <summary>
/// Base entity class containing common properties for all entities
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
