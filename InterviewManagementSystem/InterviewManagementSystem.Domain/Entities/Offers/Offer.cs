using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.Offers;


public partial class Offer : BaseEntity
{
    public string? Note { get; set; }

    public decimal? BasicSalary { get; set; }

    public DateTime? DueDate { get; set; }

    public DatePeriod? DatePeriod { get; set; }

    public OfferStatusEnum OfferStatusId { get; set; }

    public PositionEnum PositionId { get; set; }

    public Guid? CandidateId { get; set; }

    public Guid? ApproverId { get; set; }

    public Guid? RecruiterOwnerId { get; set; }

    public ContractTypeEnum ContractTypeId { get; set; }

    public LevelEnum LevelId { get; set; }

    public DepartmentEnum DepartmentId { get; set; }

    public Guid? InterviewScheduleId { get; set; }

    public virtual AppUser? Approver { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual ContractType? ContractType { get; set; }

    public virtual AppUser? CreatedByNavigation { get; set; }

    public virtual Department? Department { get; set; }

    public virtual InterviewSchedule? InterviewSchedule { get; set; }

    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = [];

    public virtual Level? Level { get; set; }

    public virtual OfferStatus? OfferStatus { get; set; }

    public virtual Position? Position { get; set; }

    public virtual AppUser? RecruiterOwner { get; set; }

    public virtual AppUser? UpdatedByNavigation { get; set; }

    public virtual CandidateOfferStatus CandidateOfferStatus { get; set; } = null!;

}




public partial class Offer
{

    public void SetCandidateOfferStatus(CandidateOfferStatus candidateOfferStatus)
    {
        CandidateOfferStatus = candidateOfferStatus;
    }



    public void SetStatus(OfferStatusEnum offerStatusEnum)
    {
        OfferStatusId = offerStatusEnum;
    }
}