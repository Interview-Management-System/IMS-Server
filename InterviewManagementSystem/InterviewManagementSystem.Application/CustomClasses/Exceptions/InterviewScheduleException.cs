using InterviewManagementSystem.Domain.Entities.Interviews;

namespace InterviewManagementSystem.Application.CustomClasses.Exceptions;

internal sealed class InterviewScheduleException(string errorMessage) : ApplicationException(errorMessage)
{

    public static void ThrowIfSetTheSameStatus(InterviewSchedule interviewSchedule, InterviewStatusEnum statusToSet)
    {

        var interviewStatusId = interviewSchedule.InterviewScheduleStatusId;

        if (interviewStatusId.HasValue is false)
            throw new InvalidOperationException("Status has no value");



        var isTheSameStatus = interviewStatusId == (short)statusToSet;

        if (isTheSameStatus)
            throw new CandidateException($"The status is already {((short)interviewStatusId).GetInterviewStatusNameById()}");
    }



    public static void ThrowIfSetTheSameResult(InterviewSchedule interviewSchedule, InterviewResultEnum resultToSet)
    {

        var interviewResultId = interviewSchedule.InterviewResultId;

        if (interviewResultId.HasValue is false)
            throw new InvalidOperationException("Result has no value");



        var isTheSameStatus = interviewResultId == (short)resultToSet;

        if (isTheSameStatus)
            throw new CandidateException($"The result is already {((short)interviewResultId).GetInterviewResultNameById()}");
    }
}
