namespace InterviewManagementSystem.Domain.Entities.AppUsers;

public partial class AppUserLogin
{
    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public Guid UserId { get; set; }

    public virtual AppUser User { get; set; } = null!;
}
