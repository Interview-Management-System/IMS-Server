namespace InterviewManagementSystem.Domain.ValueObjects.AppUsers;

public record OfferValueObject
{
    public virtual ICollection<Offer> OfferApprovers { get; set; } = new List<Offer>();

    public virtual ICollection<Offer> OfferCreatedByNavigations { get; set; } = new List<Offer>();

    public virtual ICollection<Offer> OfferRecruiterOwners { get; set; } = new List<Offer>();

    public virtual ICollection<Offer> OfferUpdatedByNavigations { get; set; } = new List<Offer>();
}
