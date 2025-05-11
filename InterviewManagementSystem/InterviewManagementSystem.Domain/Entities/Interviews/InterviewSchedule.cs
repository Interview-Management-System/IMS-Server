using InterviewManagementSystem.Domain.Enums;
using InterviewManagementSystem.Domain.Enums.Extensions;
using InterviewManagementSystem.Domain.Shared.Exceptions;
using InterviewManagementSystem.Domain.Shared.Utilities;

namespace InterviewManagementSystem.Domain.Entities.Interviews;

public partial class InterviewSchedule : BaseEntity
{

    public string? Title { get; set; }

    public DateTime? ScheduleTime { get; set; }

    public HourPeriod? HourPeriod { get; set; }

    public string? Location { get; set; }

    public string? Note { get; set; }

    public string? MeetingUrl { get; set; }

    public Guid? RecruiterOwnerId { get; set; }

    public Guid? CandidateId { get; set; }

    public Guid? JobId { get; set; }

    public InterviewStatusEnum? InterviewScheduleStatusId { get; set; }

    public InterviewResultEnum? InterviewResultId { get; set; }

    public Guid? OfferId { get; set; }

    public virtual AppUser? Candidate { get; set; }

    public virtual AppUser? CreatedByNavigation { get; set; }

    public virtual InterviewResult? InterviewResult { get; set; }

    public virtual InterviewScheduleStatus? InterviewScheduleStatus { get; set; }

    public virtual Job? Job { get; set; }

    public virtual Offer? Offer { get; set; }

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public virtual AppUser? RecruiterOwner { get; set; }

    public virtual AppUser? UpdatedByNavigation { get; set; }

    public virtual ICollection<AppUser> Interviewers { get; set; } = new List<AppUser>();

    public CandidateStatusEnum? CandidateStatusId { get; set; }

    public virtual CandidateStatus? CandidateStatus { get; set; }



    public InterviewSchedule()
    {
        SetStatus(InterviewStatusEnum.New);
        CandidateStatusId = CandidateStatusEnum.WaitingForInterview;
    }
}



#region Support methods
public partial class InterviewSchedule
{


    public void MarkAsPassed()
    {
        SetResult(InterviewResultEnum.Pass);
        CandidateStatusId = CandidateStatusEnum.PassedInterview;
        SetStatus(InterviewStatusEnum.Closed);
    }



    public void MarkAsFailed()
    {
        SetResult(InterviewResultEnum.Failed);
        CandidateStatusId = CandidateStatusEnum.FailedInterview;
        SetStatus(InterviewStatusEnum.Closed);
    }


    public void AddInterviewers(List<AppUser>? interviewers)
    {
        Interviewers.Clear();

        if (interviewers is null || interviewers.Count == 0)
        {
            return;
        }


        var interviewersToRemove = EntityComparer.GetNonMatchingEntities(Interviewers, interviewers);
        foreach (var interviewer in interviewersToRemove)
        {
            Interviewers.Remove(interviewer);
            interviewer.RemoveInterviewSchedule(this);
        }


        var interviewersToAdd = EntityComparer.GetNonMatchingEntities(interviewers, Interviewers);
        foreach (var newInterviewer in interviewersToAdd)
        {
            Interviewers.Add(newInterviewer);
            newInterviewer.AddInterviewSchedule(this);
        }

    }


    public void SetStatus(InterviewStatusEnum status)
    {
        ImsError.ThrowIfInvalidOperation(status != InterviewScheduleStatusId, "Same status");
        InterviewScheduleStatusId = status;
    }


    public void SetResult(InterviewResultEnum result)
    {
        bool isValidResult = (result != InterviewResultId) || (result.IsNotDefault());
        ImsError.ThrowIfInvalidOperation(isValidResult, "Same result");
        InterviewResultId = result;
    }


    public void SetOffer(Offer offer)
    {
        Offer = offer;
    }


    public void SetHourPeriod(HourPeriod hourPeriod)
    {
        HourPeriod = hourPeriod;
    }

}
#endregion