namespace InterviewManagementSystem.Domain.Entities.AppUsers;

public partial class CandidateStatus
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();
}
