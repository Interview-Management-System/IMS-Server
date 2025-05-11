using InterviewManagementSystem.Domain.Enums;
using InterviewManagementSystem.Domain.Shared.Exceptions;

namespace InterviewManagementSystem.Domain.Entities.AppUsers;



public partial class Candidate : AppUser
{
    public short? YearsOfExperience { get; set; }

    public Guid? RecruiterId { get; set; }

    public PositionEnum PositionId { get; set; }

    public HighestLevelEnum HighestLevelId { get; set; }

    public Guid? JobId { get; set; }

    public CandidateStatusEnum? CandidateStatusId { get; set; }

    public string? AttachmentLink { get; set; }

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

    public Candidate()
    {
        SetCandidateStatus(CandidateStatusEnum.Open);
    }


    public void SetJob(Job job)
    {
        Job = job;
    }



    public void SetCandidateStatus(CandidateStatusEnum candidateStatusEnum)
    {

        bool isValidStatus = CandidateStatusId != candidateStatusEnum;
        ImsError.ThrowIfInvalidOperation(isValidStatus, "Current candidate has the same status");

        CandidateStatusId = candidateStatusEnum;
    }



    public void AddCandidateOfferStatus(CandidateOfferStatus candidateOfferStatus)
    {
        CandidateOfferStatuses.Add(candidateOfferStatus);
    }


    public void SetSkillList(List<Skill> skills)
    {
        if (skills.Count == 0)
        {
            Skills.Clear();
        }
        else
        {
            foreach (var skill in skills)
            {
                Skills.Add(skill);
            }
        }
    }

}