using System.Diagnostics.CodeAnalysis;

namespace InterviewManagementSystem.Domain.Shared.Exceptions;

public class ImsError(string message) : Exception(message)
{

    /// <summary>
    /// Can use for check string is empty and an object is null
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="errorMessage"></param>
    public static void ThrowIfNullOrEmpty([NotNull] object? obj, string? errorMessage = "Object is null or empty")
    {
        // Check for string type
        if (obj is string str)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(str.Trim(), errorMessage);
        }
        else
        {
            ArgumentNullException.ThrowIfNull(obj, errorMessage);
        }
    }



    public static void ThrowIfInvalidOperation(bool isValidOperation, string? errorMessage = "Invalid operation")
    {
        if (isValidOperation)
        {
            throw new InvalidOperationException(errorMessage);
        }
    }
}
