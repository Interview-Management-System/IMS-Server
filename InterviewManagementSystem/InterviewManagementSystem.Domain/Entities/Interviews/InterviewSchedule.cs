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

    public short? InterviewScheduleStatusId { get; set; }

    public short? InterviewResultId { get; set; }

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
}
