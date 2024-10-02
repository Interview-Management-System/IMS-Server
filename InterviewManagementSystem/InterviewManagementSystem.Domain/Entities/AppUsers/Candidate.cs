using InterviewManagementSystem.Domain.Enums;

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






public partial class Candidate
{


    public void SetCandidateStatus(CandidateStatusEnum candidateStatusEnum)
    {

        switch (candidateStatusEnum)
        {
            case CandidateStatusEnum.Open:
                CandidateStatusId = (short)CandidateStatusEnum.Open;
                break;


            case CandidateStatusEnum.Banned:
                CandidateStatusId = (short)CandidateStatusEnum.Banned;
                break;


            case CandidateStatusEnum.WaitingForInterview:
                CandidateStatusId = (short)CandidateStatusEnum.WaitingForInterview;
                break;


            case CandidateStatusEnum.InProgress:
                CandidateStatusId = (short)CandidateStatusEnum.InProgress;
                break;


            case CandidateStatusEnum.Cancelled:
                CandidateStatusId = (short)CandidateStatusEnum.Cancelled;
                break;


            case CandidateStatusEnum.FailedInterview:
                CandidateStatusId = (short)CandidateStatusEnum.FailedInterview;
                break;


            case CandidateStatusEnum.PassedInterview:
                CandidateStatusId = (short)CandidateStatusEnum.PassedInterview;
                break;


            case CandidateStatusEnum.WaitingForApproval:
                CandidateStatusId = (short)CandidateStatusEnum.WaitingForApproval;
                break;


            case CandidateStatusEnum.RejectedOffer:
                CandidateStatusId = (short)CandidateStatusEnum.RejectedOffer;
                break;


            case CandidateStatusEnum.ApprovedOffer:
                CandidateStatusId = (short)CandidateStatusEnum.ApprovedOffer;
                break;


            case CandidateStatusEnum.CancelledOffer:
                CandidateStatusId = (short)CandidateStatusEnum.CancelledOffer;
                break;


            case CandidateStatusEnum.WaitingForResponse:
                CandidateStatusId = (short)CandidateStatusEnum.WaitingForResponse;
                break;


            case CandidateStatusEnum.AcceptedOffer:
                CandidateStatusId = (short)CandidateStatusEnum.AcceptedOffer;
                break;


            case CandidateStatusEnum.DeclinedOffer:
                CandidateStatusId = (short)CandidateStatusEnum.DeclinedOffer;
                break;

            default:
                break;
        }

    }
}