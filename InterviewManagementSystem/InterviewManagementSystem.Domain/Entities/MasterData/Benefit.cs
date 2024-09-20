using InterviewManagementSystem.Domain.Entities.Jobs;

namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class Benefit
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
