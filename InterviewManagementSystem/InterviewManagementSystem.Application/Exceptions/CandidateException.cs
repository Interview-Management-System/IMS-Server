using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Exceptions;

internal class CandidateException(string errorMessage) : ApplicationException(errorMessage)
{

    public static void ThrowIfSetTheSameStatus(Candidate candidate, CandidateStatusEnum statusToSet)
    {
        var candidateStatusId = candidate.CandidateStatusId;


        if (candidateStatusId.HasValue is false)
            throw new InvalidOperationException("Status has no value");



        var isTheSameStatus = candidateStatusId == (short)statusToSet;

        if (isTheSameStatus)
            throw new CandidateException($"Candidate status is already {((short)candidateStatusId).GetCandidateStatusNameById()}");
    }
}
