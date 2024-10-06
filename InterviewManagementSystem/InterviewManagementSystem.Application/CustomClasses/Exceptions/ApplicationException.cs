namespace InterviewManagementSystem.Application.CustomClasses.Exceptions;

public class ApplicationException(string errorMessage) : Exception(errorMessage)
{


    internal static void ThrowIfOperationFail(bool isOperationSuccess, string? errorMessage = null)
    {
        if (isOperationSuccess == false)
            throw new ApplicationException(errorMessage ?? "Operation fail");
    }


    internal static void ThrowIfGetDeletedRecord(bool isDeleted)
    {
        if (isDeleted is true)
            throw new AppUserException("Data is already deleted, can not proceed");
    }
}
