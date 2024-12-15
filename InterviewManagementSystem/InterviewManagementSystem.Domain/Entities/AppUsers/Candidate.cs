using InterviewManagementSystem.Domain.Enums;
using InterviewManagementSystem.Domain.Exceptions;

namespace InterviewManagementSystem.Domain.Entities.AppUsers;



public partial class Candidate : AppUser
{
    public short? YearsOfExperience { get; set; }

    public byte[]? Attachment { get; set; }

    public Guid? RecruiterId { get; set; }

    public PositionEnum PositionId { get; set; }

    public HighestLevelEnum HighestLevelId { get; set; }

    public Guid? JobId { get; set; }

    public CandidateStatusEnum CandidateStatusId { get; set; }

    public virtual CandidateStatus? CandidateStatus { get; set; }

    public virtual HighestLevel? HighestLevel { get; set; }

    public virtual AppUser IdNavigation { get; set; } = null!;

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public virtual Job? Job { get; set; }

    public virtual Position? Position { get; set; }

    public virtual AppUser? Recruiter { get; set; }

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public virtual ICollection<CandidateOfferStatus> CandidateOfferStatuses { get; set; } = new List<CandidateOfferStatus>();
}






public partial class Candidate
{




    public void SetJob(Job job)
    {
        Job = job;
    }



    public void SetCandidateStatus(CandidateStatusEnum candidateStatusEnum)
    {

        bool isInvalidStatus = CandidateStatusId == candidateStatusEnum;
        DomainException.ThrowIfInvalidOperation(isInvalidStatus, "Current candidate has the same status");

        CandidateStatusId = candidateStatusEnum;
    }



    public void AddCandidateOfferStatus(CandidateOfferStatus candidateOfferStatus)
    {
        CandidateOfferStatuses.Add(candidateOfferStatus);
    }

}