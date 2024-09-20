namespace InterviewManagementSystem.Domain.Entities.AppUsers;

public partial class AppRoleClaim
{
    public int Id { get; set; }

    public Guid RoleId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual AppRole Role { get; set; } = null!;
}
