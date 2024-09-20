namespace InterviewManagementSystem.Domain.Entities.Offers;

public partial class OfferStatus
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
