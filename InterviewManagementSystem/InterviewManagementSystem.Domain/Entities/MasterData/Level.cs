using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.Offers;

namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class Level
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
