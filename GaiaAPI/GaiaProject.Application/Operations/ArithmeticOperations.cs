using GaiaProject.Application.Interfaces;

namespace GaiaProject.Application.Operations;

// A34D - Arithmetic Operation Executors

/// <summary>
/// Addition operation executor
/// </summary>
public class AdditionOperationExecutor : IOperationExecutor
{
    public string OperationType => "Addition";

    public string Execute(string fieldA, string fieldB)
    {
        if (double.TryParse(fieldA, out var numA) && double.TryParse(fieldB, out var numB))
        {
            return (numA + numB).ToString();
        }
        throw new InvalidOperationException("Both fields must be valid numbers for addition");
    }
}

/// <summary>
/// Subtraction operation executor
/// </summary>
public class SubtractionOperationExecutor : IOperationExecutor
{
    public string OperationType => "Subtraction";

    public string Execute(string fieldA, string fieldB)
    {
        if (double.TryParse(fieldA, out var numA) && double.TryParse(fieldB, out var numB))
        {
            return (numA - numB).ToString();
        }
        throw new InvalidOperationException("Both fields must be valid numbers for subtraction");
    }
}

/// <summary>
/// Multiplication operation executor
/// </summary>
public class MultiplicationOperationExecutor : IOperationExecutor
{
    public string OperationType => "Multiplication";

    public string Execute(string fieldA, string fieldB)
    {
        if (double.TryParse(fieldA, out var numA) && double.TryParse(fieldB, out var numB))
        {
            return (numA * numB).ToString();
        }
        throw new InvalidOperationException("Both fields must be valid numbers for multiplication");
    }
}

/// <summary>
/// Division operation executor
/// </summary>
public class DivisionOperationExecutor : IOperationExecutor
{
    public string OperationType => "Division";

    public string Execute(string fieldA, string fieldB)
    {
        if (double.TryParse(fieldA, out var numA) && double.TryParse(fieldB, out var numB))
        {
            if (numB == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            
            return (numA / numB).ToString();
        }
        throw new InvalidOperationException("Both fields must be valid numbers for division");
    }
}

/// <summary>
/// Modulo operation executor
/// </summary>
public class ModuloOperationExecutor : IOperationExecutor
{
    public string OperationType => "Modulo";

    public string Execute(string fieldA, string fieldB)
    {
        if (double.TryParse(fieldA, out var numA) && double.TryParse(fieldB, out var numB))
        {
            if (numB == 0)
                throw new DivideByZeroException("Cannot calculate modulo with zero divisor");
            
            return (numA % numB).ToString();
        }
        throw new InvalidOperationException("Both fields must be valid numbers for modulo");
    }
}

/// <summary>
/// Power operation executor
/// </summary>
public class PowerOperationExecutor : IOperationExecutor
{
    public string OperationType => "Power";

    public string Execute(string fieldA, string fieldB)
    {
        if (double.TryParse(fieldA, out var numA) && double.TryParse(fieldB, out var numB))
        {
            return Math.Pow(numA, numB).ToString();
        }
        throw new InvalidOperationException("Both fields must be valid numbers for power operation");
    }
}
