namespace InterviewManagementSystem.Domain.Entities.Interviews;

public partial class InterviewScheduleStatus
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();
}
