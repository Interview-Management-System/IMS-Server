namespace InterviewManagementSystem.Domain.Entities.Jobs;

public partial class Job : BaseEntity
{
    public string? Title { get; set; }

    public string? WorkingAddress { get; set; }

    public string? Description { get; set; }

    public SalaryRange? SalaryRange { get; set; }

    public DatePeriod? DatePeriod { get; set; }

    public Guid? AppUserId { get; set; }

    public short? JobStatusId { get; set; }

    public virtual AppUser? AppUser { get; set; }

    public virtual AppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();

    public virtual JobStatus? JobStatus { get; set; }

    public virtual AppUser? UpdatedByNavigation { get; set; }

    public virtual ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();

    public virtual ICollection<Level> Levels { get; set; } = new List<Level>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
