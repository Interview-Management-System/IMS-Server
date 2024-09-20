using InterviewManagementSystem.Domain.Entities.Offers;

namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class Department
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
