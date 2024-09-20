namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class ContractType
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
