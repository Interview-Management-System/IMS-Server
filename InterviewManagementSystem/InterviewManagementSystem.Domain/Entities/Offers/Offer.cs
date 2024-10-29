using InterviewManagementSystem.Domain.Aggregates;
using InterviewManagementSystem.Domain.CustomClasses;
using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.Offers;


public partial class Offer : BaseEntity, IAggregate<Guid>
{
    public string? Note { get; set; }

    public decimal? BasicSalary { get; set; }

    public DateTime? DueDate { get; set; }

    public DatePeriod? DatePeriod { get; set; }

    public short? OfferStatusId { get; set; }

    public short? PositionId { get; set; }

    public Guid? CandidateId { get; set; }

    public Guid? ApproverId { get; set; }

    public Guid? RecruiterOwnerId { get; set; }

    public short? ContractTypeId { get; set; }

    public short? LevelId { get; set; }

    public short? DepartmentId { get; set; }

    public Guid? InterviewScheduleId { get; set; }

    public virtual AppUser? Approver { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual ContractType? ContractType { get; set; }

    public virtual AppUser? CreatedByNavigation { get; set; }

    public virtual Department? Department { get; set; }

    public virtual InterviewSchedule? InterviewSchedule { get; set; }

    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();

    public virtual Level? Level { get; set; }

    public virtual OfferStatus? OfferStatus { get; set; }

    public virtual Position? Position { get; set; }

    public virtual AppUser? RecruiterOwner { get; set; }

    public virtual AppUser? UpdatedByNavigation { get; set; }

    public virtual CandidateOfferStatus CandidateOfferStatus { get; set; } = null!;


    private Offer()
    {
        GenerateId();
    }
}




public partial class Offer
{


    private void GenerateId()
    {
        Id = Guid.NewGuid();
    }


    public void SetCandidateOfferStatus(CandidateOfferStatus candidateOfferStatus)
    {
        CandidateOfferStatus = candidateOfferStatus;
    }



    public void SetStatus(OfferStatusEnum offerStatusEnum)
    {
        OfferStatusId = (short)offerStatusEnum;
    }



    public static Offer Create(DataForCreateOffer dataToCreate)
    {

        var associatedCandidate = dataToCreate.AssociatedCandidate;
        var associatedInterviewSchedule = dataToCreate.AssociatedInterviewSchedule;


        var newOffer = new Offer()
        {
            Note = dataToCreate.Note,
            LevelId = dataToCreate.LevelId,
            ApproverId = dataToCreate.ApproverId,
            CandidateId = dataToCreate.CandidateId,
            DueDate = dataToCreate.DueDate,
            PositionId = dataToCreate.PositionId,
            DepartmentId = dataToCreate.DepartmentId,
            BasicSalary = dataToCreate.BasicSalary,
            ContractTypeId = dataToCreate.ContractTypeId,
            RecruiterOwnerId = dataToCreate.RecruiterOwnerId,
            InterviewScheduleId = dataToCreate.InterviewScheduleId,
            DatePeriod = dataToCreate.DatePeriod,
            Candidate = associatedCandidate,
            InterviewSchedule = associatedInterviewSchedule,
        };


        var candidateOfferStatus = CandidateOfferStatus.Create(dataToCreate.CandidateId, newOffer.Id);

        // Ensure transactional business logic
        associatedInterviewSchedule.SetOffer(newOffer);
        newOffer.SetCandidateOfferStatus(candidateOfferStatus);
        newOffer.SetStatus(OfferStatusEnum.WaitingForApproval);
        associatedCandidate.AddCandidateOfferStatus(candidateOfferStatus);


        return newOffer;
    }




}