namespace InterviewManagementSystem.Domain.Entities.AppUsers;

public partial class AppUserClaim
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual AppUser User { get; set; } = null!;
}
