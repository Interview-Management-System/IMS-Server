using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class HighestLevel
{
    public HighestLevelEnum Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
}
