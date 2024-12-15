using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class Department
{
    public DepartmentEnum Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
