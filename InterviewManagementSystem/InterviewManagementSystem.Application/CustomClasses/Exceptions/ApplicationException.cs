namespace InterviewManagementSystem.Application.CustomClasses.Exceptions;

public class ApplicationException(string errorMessage) : Exception(errorMessage)
{


    internal static void ThrowIfOperationFail(bool isOperationSuccess, string? errorMessage = null)
    {
        if (isOperationSuccess == false)
            throw new ApplicationException(errorMessage ?? "Action fail");
    }


    internal static void ThrowIfGetDeletedRecord(bool isDeleted)
    {
        if (isDeleted is true)
            throw new AppUserException("Data is already deleted, can not proceed");
    }


    internal static void ThrowIfInvalidOperation(bool isInvalidCondition, string? errorMessage = null)
    {
        if (isInvalidCondition)
            throw new InvalidOperationException(errorMessage ?? "Invalid operation");
    }


    internal static void ThrowIfNoRecordFound(bool isAnyRecordFound, string? errorMessage = null)
    {
        if (isAnyRecordFound is false)
            throw new ApplicationException(errorMessage ?? "Invalid operation");
    }
}
