namespace InterviewManagementSystem.Domain.ValueObjects.AppUsers;

public record InterviewScheduleValueObject
{
    public virtual ICollection<InterviewSchedule> InterviewScheduleCandidates { get; set; } = new List<InterviewSchedule>();

    public virtual ICollection<InterviewSchedule> InterviewScheduleCreatedByNavigations { get; set; } = new List<InterviewSchedule>();

    public virtual ICollection<InterviewSchedule> InterviewScheduleRecruiterOwners { get; set; } = new List<InterviewSchedule>();

    public virtual ICollection<InterviewSchedule> InterviewScheduleUpdatedByNavigations { get; set; } = new List<InterviewSchedule>();
    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();
}
