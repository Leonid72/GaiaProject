using GaiaProject.Application.Interfaces;
using GaiaProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaiaProject.Infrastructure.Data;

// A34D - Application DbContext
/// <summary>
/// Entity Framework DbContext for Gaia Project
/// Manages database connection and entity configurations
/// </summary>
public class GaiaDbContext : DbContext, IGaiaDbContext
{
    public GaiaDbContext(DbContextOptions<GaiaDbContext> options) : base(options)
    {
    }

    public DbSet<Operation> Operations => Set<Operation>();
    public DbSet<OperationHistory> OperationHistories => Set<OperationHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Operation Entity Configuration
        modelBuilder.Entity<Operation>(entity =>
        {
            entity.ToTable("Operations");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .HasMaxLength(500);
            
            entity.Property(e => e.OperationType)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.ImplementationClass)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasIndex(e => e.Name)
                .IsUnique();
            
            entity.HasIndex(e => e.OperationType);
            entity.HasIndex(e => e.IsActive);
        });

        // OperationHistory Entity Configuration
        modelBuilder.Entity<OperationHistory>(entity =>
        {
            entity.ToTable("OperationHistories");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.FieldA)
                .IsRequired()
                .HasMaxLength(1000);
            
            entity.Property(e => e.FieldB)
                .IsRequired()
                .HasMaxLength(1000);
            
            entity.Property(e => e.Result)
                .HasMaxLength(2000);
            
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(2000);
            
            entity.Property(e => e.ClientInfo)
                .HasMaxLength(500);
            
            entity.Property(e => e.ExecutedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasOne(e => e.Operation)
                .WithMany(o => o.OperationHistories)
                .HasForeignKey(e => e.OperationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasIndex(e => e.OperationId);
            entity.HasIndex(e => e.ExecutedAt);
            entity.HasIndex(e => e.IsSuccess);
        });

        // Seed initial data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // A34D - Seed initial operations
        modelBuilder.Entity<Operation>().HasData(
            new Operation
            {
                Id = 1,
                Name = "Addition",
                DisplayName = "Addition (+)",
                Description = "Adds two numbers together",
                OperationType = "Arithmetic",
                ImplementationClass = "AdditionOperationExecutor",
                IsActive = true,
                SortOrder = 1,
                CreatedAt = DateTime.UtcNow
            },
            new Operation
            {
                Id = 2,
                Name = "Subtraction",
                DisplayName = "Subtraction (-)",
                Description = "Subtracts the second number from the first",
                OperationType = "Arithmetic",
                ImplementationClass = "SubtractionOperationExecutor",
                IsActive = true,
                SortOrder = 2,
                CreatedAt = DateTime.UtcNow
            },
            new Operation
            {
                Id = 3,
                Name = "Multiplication",
                DisplayName = "Multiplication (×)",
                Description = "Multiplies two numbers",
                OperationType = "Arithmetic",
                ImplementationClass = "MultiplicationOperationExecutor",
                IsActive = true,
                SortOrder = 3,
                CreatedAt = DateTime.UtcNow
            },
            new Operation
            {
                Id = 4,
                Name = "Division",
                DisplayName = "Division (÷)",
                Description = "Divides the first number by the second",
                OperationType = "Arithmetic",
                ImplementationClass = "DivisionOperationExecutor",
                IsActive = true,
                SortOrder = 4,
                CreatedAt = DateTime.UtcNow
            },
            new Operation
            {
                Id = 5,
                Name = "Modulo",
                DisplayName = "Modulo (%)",
                Description = "Returns the remainder of division",
                OperationType = "Arithmetic",
                ImplementationClass = "ModuloOperationExecutor",
                IsActive = true,
                SortOrder = 5,
                CreatedAt = DateTime.UtcNow
            },
            new Operation
            {
                Id = 6,
                Name = "Power",
                DisplayName = "Power (^)",
                Description = "Raises the first number to the power of the second",
                OperationType = "Arithmetic",
                ImplementationClass = "PowerOperationExecutor",
                IsActive = true,
                SortOrder = 6,
                CreatedAt = DateTime.UtcNow
            },
            new Operation
            {
                Id = 7,
                Name = "Concatenation",
                DisplayName = "Concatenation (String)",
                Description = "Joins two strings together",
                OperationType = "String",
                ImplementationClass = "ConcatenationOperationExecutor",
                IsActive = true,
                SortOrder = 7,
                CreatedAt = DateTime.UtcNow
            },
            new Operation
            {
                Id = 8,
                Name = "Compare",
                DisplayName = "Compare (String)",
                Description = "Compares two strings",
                OperationType = "String",
                ImplementationClass = "CompareOperationExecutor",
                IsActive = true,
                SortOrder = 8,
                CreatedAt = DateTime.UtcNow
            },
            new Operation
            {
                Id = 9,
                Name = "Contains",
                DisplayName = "Contains (String)",
                Description = "Checks if first string contains the second",
                OperationType = "String",
                ImplementationClass = "ContainsOperationExecutor",
                IsActive = true,
                SortOrder = 9,
                CreatedAt = DateTime.UtcNow
            },
            new Operation
            {
                Id = 10,
                Name = "LengthCompare",
                DisplayName = "Length Compare (String)",
                Description = "Compares the lengths of two strings",
                OperationType = "String",
                ImplementationClass = "LengthCompareOperationExecutor",
                IsActive = true,
                SortOrder = 10,
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
