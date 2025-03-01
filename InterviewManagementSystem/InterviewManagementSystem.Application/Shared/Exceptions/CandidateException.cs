namespace InterviewManagementSystem.Application.Shared.Exceptions;

internal class CandidateException(string errorMessage) : ApplicationException(errorMessage)
{

    public static void ThrowIfSetTheSameStatus(CandidateStatusEnum currentStatus, CandidateStatusEnum statusToSet)
    {
        var isTheSameStatus = currentStatus == statusToSet;

        if (isTheSameStatus)
            throw new CandidateException($"Candidate status is already {statusToSet.GetDescription()}");
    }
}
