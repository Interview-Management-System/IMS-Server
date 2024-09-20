using InterviewManagementSystem.Domain.Entities.Offers;

namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class Position
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
