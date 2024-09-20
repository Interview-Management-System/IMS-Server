using Microsoft.AspNetCore.Identity;

namespace InterviewManagementSystem.Domain.Entities.AppUsers;

public partial class AppRole : IdentityRole<Guid>
{

    public virtual ICollection<AppRoleClaim> AppRoleClaims { get; set; } = new List<AppRoleClaim>();

    public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
}
