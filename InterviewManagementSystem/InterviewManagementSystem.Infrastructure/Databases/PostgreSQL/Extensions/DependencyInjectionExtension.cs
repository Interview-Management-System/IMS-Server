using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Infrastructure.Persistences;
using InterviewManagementSystem.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.Extensions;

public static class DependencyInjectionExtension
{

    public static void AddPostgreSqlDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["IMS_PostgreSqlSetting:ConnectionString"];
        ArgumentNullException.ThrowIfNullOrEmpty(connectionString, "Connection string not found");

        // Setup DB
        services.AddDbContext<InterviewManagementSystemContext>(options => options.UseNpgsql(connectionString));

        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<InterviewManagementSystemContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
