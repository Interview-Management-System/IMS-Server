using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Infrastructure.Persistences;
using InterviewManagementSystem.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.API.Configurations;

internal static class InjectionService
{

    internal static void AddInjectionService(this IServiceCollection services)
    {

        // Setup DB
        const string connection = "Host=localhost;Database=InterviewManagementSystem;Username=postgres;Password=sa";

        services.AddDbContext<InterviewManagementSystemContext>(options => options.UseNpgsql(connection));

        services.AddIdentity<AppUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<InterviewManagementSystemContext>()
            .AddDefaultTokenProviders();



        services.AddSingleton<UnitOfWork>();
    }
}
