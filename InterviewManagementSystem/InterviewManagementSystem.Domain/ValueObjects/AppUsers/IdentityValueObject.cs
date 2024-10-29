namespace InterviewManagementSystem.Domain.ValueObjects.AppUsers;

public record IdentityValueObject
{
    public virtual ICollection<AppUserClaim> AppUserClaims { get; set; } = new List<AppUserClaim>();

    public virtual ICollection<AppUserLogin> AppUserLogins { get; set; } = new List<AppUserLogin>();

    public virtual ICollection<AppUserToken> AppUserTokens { get; set; } = new List<AppUserToken>();
}
