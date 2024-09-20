namespace InterviewManagementSystem.Domain.Entities.AppUsers;

public partial class Candidate : AppUser
{
    public short? YearsOfExperience { get; set; }

    public uint? Attachment { get; set; }

    public Guid? RecruiterId { get; set; }

    public short? PositionId { get; set; }

    public short? HighestLevelId { get; set; }

    public short? CandidateStatusId { get; set; }

    public virtual CandidateStatus? CandidateStatus { get; set; }

    public virtual HighestLevel? HighestLevel { get; set; }

    public virtual AppUser IdNavigation { get; set; } = null!;

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public virtual Position? Position { get; set; }

    public virtual AppUser? Recruiter { get; set; }

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
