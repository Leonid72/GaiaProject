using GaiaProject.Application.Interfaces;

namespace GaiaProject.Application.Operations;

// A34D - String Operation Executors

/// <summary>
/// Concatenation operation executor
/// </summary>
public class ConcatenationOperationExecutor : IOperationExecutor
{
    public string OperationType => "Concatenation";

    public string Execute(string fieldA, string fieldB)
    {
        return fieldA + fieldB;
    }
}

/// <summary>
/// String comparison operation executor
/// </summary>
public class CompareOperationExecutor : IOperationExecutor
{
    public string OperationType => "Compare";

    public string Execute(string fieldA, string fieldB)
    {
        var result = string.Compare(fieldA, fieldB, StringComparison.Ordinal);
        return result == 0 ? "Equal" : result < 0 ? "FieldA is less than FieldB" : "FieldA is greater than FieldB";
    }
}

/// <summary>
/// String contains operation executor
/// </summary>
public class ContainsOperationExecutor : IOperationExecutor
{
    public string OperationType => "Contains";

    public string Execute(string fieldA, string fieldB)
    {
        //return fieldA.Contains(fieldB, StringComparison.OrdinalIgnoreCase).ToString();
        bool aContainsB = fieldA.Contains(fieldB, StringComparison.OrdinalIgnoreCase);
        bool bContainsA = fieldB.Contains(fieldA, StringComparison.OrdinalIgnoreCase);

        return (aContainsB || bContainsA).ToString();
    }
}

/// <summary>
/// String replace operation executor
/// </summary>
public class ReplaceOperationExecutor : IOperationExecutor
{
    public string OperationType => "Replace";

    public string Execute(string fieldA, string fieldB)
    {
        // FieldA contains the string to search in, FieldB is what to replace
        // Format: "oldValue|newValue" in FieldB
        var parts = fieldB.Split('|');
        if (parts.Length != 2)
            throw new InvalidOperationException("FieldB must be in format 'oldValue|newValue'");
        
        return fieldA.Replace(parts[0], parts[1]);
    }
}

/// <summary>
/// String length comparison operation executor
/// </summary>
public class LengthCompareOperationExecutor : IOperationExecutor
{
    public string OperationType => "LengthCompare";

    public string Execute(string fieldA, string fieldB)
    {
        var lengthA = fieldA.Length;
        var lengthB = fieldB.Length;
        return $"FieldA length: {lengthA}, FieldB length: {lengthB}, Difference: {lengthA - lengthB}";
    }
}
