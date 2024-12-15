using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.Offers;

public partial class OfferStatus
{
    public OfferStatusEnum Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Offer> Offers { get; set; } = [];
}
