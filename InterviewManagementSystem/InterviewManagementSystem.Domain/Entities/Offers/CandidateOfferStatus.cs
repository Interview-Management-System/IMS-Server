using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.Offers;

public partial class CandidateOfferStatus
{
    public Guid CandidateId { get; set; }

    public Guid OfferId { get; set; }

    public short CandidateStatusId { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual Offer? Offer { get; set; }


    private CandidateOfferStatus()
    {
        CandidateStatusId = (short)CandidateStatusEnum.WaitingForApproval;
    }
}



public partial class CandidateOfferStatus
{


    public static CandidateOfferStatus Create(Guid candidateId, Guid offerId)
    {
        return new CandidateOfferStatus
        {
            CandidateId = candidateId,
            OfferId = offerId,
        };
    }
}