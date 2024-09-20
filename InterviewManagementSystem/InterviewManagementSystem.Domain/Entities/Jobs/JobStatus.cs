namespace InterviewManagementSystem.Domain.Entities.Jobs;

public partial class JobStatus
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
