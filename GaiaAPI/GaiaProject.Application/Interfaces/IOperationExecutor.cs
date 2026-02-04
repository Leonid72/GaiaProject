namespace GaiaProject.Application.Interfaces;

// A34D - Operation Executor Interface
/// <summary>
/// Interface for implementing different operation types
/// Supports Strategy Pattern for extensible operations
/// </summary>
public interface IOperationExecutor
{
    string OperationType { get; }
    string Execute(string fieldA, string fieldB);
}
