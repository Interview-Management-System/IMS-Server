using InterviewManagementSystem.Domain.Aggregates;
using InterviewManagementSystem.Domain.CustomClasses.EntityData.InterviewData;
using InterviewManagementSystem.Domain.CustomClasses.Utilities;
using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.Interviews;

public partial class InterviewSchedule : BaseEntity, IAggregate<Guid>
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
        GenerateId();
        SetStatus(InterviewStatusEnum.New);
    }


    private void GenerateId()
    {
        Id = Guid.NewGuid();
    }
}



#region Aggregate methods
public partial class InterviewSchedule
{
    public static InterviewSchedule Create(DataForCreateInterview dataForCreateInterview)
    {



        var newInterviewSchedule = new InterviewSchedule()
        {
            Title = dataForCreateInterview.Title,
            CandidateId = dataForCreateInterview.CandidateId,
            ScheduleTime = dataForCreateInterview.ScheduleTime,
            Note = dataForCreateInterview.Note,
            Location = dataForCreateInterview.Location,
            JobId = dataForCreateInterview.JobId,
            MeetingUrl = dataForCreateInterview.MeetingUrl,
            RecruiterOwnerId = dataForCreateInterview.RecruiterOwnerId,
            CandidateStatusId = CandidateStatusEnum.WaitingForInterview,
        };


        var hourPeriod = HourPeriod.CreatePeriod(dataForCreateInterview.StartHourString, dataForCreateInterview.EndHourString);

        newInterviewSchedule.SetHourPeriod(hourPeriod);
        newInterviewSchedule.SetInterviewers(dataForCreateInterview.Interviews);

        return newInterviewSchedule;
    }




    public static void Update(InterviewSchedule interviewSchedule, DataForUpdateInterview dataForUpdateInterview)
    {


        interviewSchedule.Note = dataForUpdateInterview.Note;
        interviewSchedule.Title = dataForUpdateInterview.Title;
        interviewSchedule.JobId = dataForUpdateInterview.JobId;
        interviewSchedule.Location = dataForUpdateInterview.Location;
        interviewSchedule.MeetingUrl = dataForUpdateInterview.MeetingUrl;
        interviewSchedule.CandidateId = dataForUpdateInterview.CandidateId;
        interviewSchedule.ScheduleTime = dataForUpdateInterview.ScheduleTime;
        interviewSchedule.RecruiterOwnerId = dataForUpdateInterview.RecruiterOwnerId;
        interviewSchedule.CandidateStatusId = dataForUpdateInterview.CandidateStatusId;


        var hourPeriod = HourPeriod.CreatePeriod(dataForUpdateInterview.StartHourString, dataForUpdateInterview.EndHourString);

        interviewSchedule.SetHourPeriod(hourPeriod);
        interviewSchedule.SetInterviewers(dataForUpdateInterview.Interviews);

    }
}
#endregion




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


    public void SetInterviewers(List<AppUser>? interviewers)
    {

        if (interviewers is null || interviewers.Count == 0)
        {
            Interviewers.Clear();
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


    public void SetStatus(InterviewStatusEnum interviewStatusEnum)
    {
        InterviewScheduleStatusId = interviewStatusEnum;
    }


    public void SetResult(InterviewResultEnum interviewResultEnum)
    {
        InterviewResultId = interviewResultEnum;
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