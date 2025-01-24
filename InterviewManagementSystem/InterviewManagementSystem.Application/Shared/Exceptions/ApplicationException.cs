namespace InterviewManagementSystem.Application.Shared.Exceptions;

public class ApplicationException(string? errorMessage) : Exception(errorMessage)
{

    public static void ThrowIfOperationFail(bool isOperationSuccess, string? errorMessage = "Action fail")
    {
        if (isOperationSuccess == false)
            throw new ApplicationException(errorMessage);
    }


    internal static void ThrowIfGetDeletedRecord(bool isDeleted)
    {
        if (isDeleted)
            throw new AppUserException("Data is already deleted, can not proceed");
    }


    internal static void ThrowIfInvalidOperation(bool isInvalidCondition, string? errorMessage = "Invalid operation")
    {
        if (isInvalidCondition)
            throw new InvalidOperationException(errorMessage);
    }


    internal static void ThrowIfNoRecordFound(bool isNoRecordFound, string? errorMessage = "No data found")
    {
        if (isNoRecordFound)
            throw new ApplicationException(errorMessage);
    }
}
