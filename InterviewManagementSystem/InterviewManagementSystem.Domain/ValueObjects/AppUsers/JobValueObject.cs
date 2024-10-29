namespace InterviewManagementSystem.Domain.ValueObjects.AppUsers;

public record JobValueObject
{
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual ICollection<Job> JobCreatedByNavigations { get; set; } = new List<Job>();

    public virtual ICollection<Job> JobUpdatedByNavigations { get; set; } = new List<Job>();
}
