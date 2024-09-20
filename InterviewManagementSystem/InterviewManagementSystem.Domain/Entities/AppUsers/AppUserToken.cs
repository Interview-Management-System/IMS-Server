namespace InterviewManagementSystem.Domain.Entities.AppUsers;

public partial class AppUserToken
{
    public Guid UserId { get; set; }

    public string LoginProvider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public virtual AppUser User { get; set; } = null!;
}
