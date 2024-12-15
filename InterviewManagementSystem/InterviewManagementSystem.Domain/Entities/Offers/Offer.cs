using InterviewManagementSystem.Domain.Aggregates;
using InterviewManagementSystem.Domain.CustomClasses.EntityData.OfferData;
using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.Offers;


public partial class Offer : BaseEntity, IAggregate<Guid>
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
        OfferStatusId = offerStatusEnum;
    }



    public static Offer Create(BaseOfferData dataToCreate)
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
        //associatedInterviewSchedule.SetOffer(newOffer);
        newOffer.SetCandidateOfferStatus(candidateOfferStatus);
        newOffer.SetStatus(OfferStatusEnum.WaitingForApproval);
        associatedCandidate.AddCandidateOfferStatus(candidateOfferStatus);


        return newOffer;
    }



    public static void Update(Offer offerToUpdate, DataForUpdateOffer dataForUpdateOffer)
    {

        var associatedCandidate = dataForUpdateOffer.AssociatedCandidate;
        var associatedInterviewSchedule = dataForUpdateOffer.AssociatedInterviewSchedule;


        offerToUpdate.Note = dataForUpdateOffer.Note;
        offerToUpdate.LevelId = dataForUpdateOffer.LevelId;
        offerToUpdate.ApproverId = dataForUpdateOffer.ApproverId;
        offerToUpdate.CandidateId = dataForUpdateOffer.CandidateId;
        offerToUpdate.DueDate = dataForUpdateOffer.DueDate;
        offerToUpdate.PositionId = PositionEnum.BackendDeveloper;
        offerToUpdate.DepartmentId = dataForUpdateOffer.DepartmentId;
        offerToUpdate.BasicSalary = dataForUpdateOffer.BasicSalary;
        offerToUpdate.ContractTypeId = dataForUpdateOffer.ContractTypeId;
        offerToUpdate.RecruiterOwnerId = dataForUpdateOffer.RecruiterOwnerId;
        offerToUpdate.InterviewScheduleId = dataForUpdateOffer.InterviewScheduleId;
        offerToUpdate.DatePeriod = dataForUpdateOffer.DatePeriod;
        offerToUpdate.Candidate = associatedCandidate;
        offerToUpdate.InterviewSchedule = associatedInterviewSchedule;


        associatedInterviewSchedule.SetOffer(offerToUpdate);
        offerToUpdate.CandidateOfferStatus.SetCandidateId(associatedCandidate.Id);
    }
}