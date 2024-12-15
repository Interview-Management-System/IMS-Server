using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Enums.Extensions;

namespace InterviewManagementSystem.Application.CustomClasses.Exceptions;

internal class CandidateException(string errorMessage) : ApplicationException(errorMessage)
{

    public static void ThrowIfSetTheSameStatus(Candidate candidate, CandidateStatusEnum statusToSet)
    {
        var candidateStatusId = candidate.CandidateStatusId;


        var isTheSameStatus = candidateStatusId == statusToSet;

        if (isTheSameStatus)
            throw new CandidateException($"Candidate status is already {statusToSet.GetEnumName()}");
    }
}
