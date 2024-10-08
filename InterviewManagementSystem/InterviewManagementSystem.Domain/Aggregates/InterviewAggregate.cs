namespace InterviewManagementSystem.Domain.Aggregates;

public sealed class InterviewAggregate
{
    public string? Title { get; set; }
    public Guid? CandidateId { get; set; }
    public DateTime ScheduleTime { get; set; }
    public HourPeriod? HourPeriod { get; set; }
    public string? Note { get; set; }
    public string? Location { get; set; }
    public Guid? JobId { get; set; }
    public List<AppUser> Interviewers { get; set; } = [];
    public string? MeetingUrl { get; set; }
    public Guid? RecruiterOwnerId { get; set; }



}
