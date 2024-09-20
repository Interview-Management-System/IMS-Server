using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Persistences.EntityConfigurations;

public static class IdentityConfiguration
{

    public static void ConfigureIdentity(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("AppRoles", "IMS");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles", "IMS");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins", "IMS");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens", "IMS");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims", "IMS");


        modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(a => new { a.LoginProvider, a.ProviderKey });
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(a => new { a.UserId, a.RoleId });
        modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(a => new { a.UserId, a.LoginProvider, a.Name });
    }
}
