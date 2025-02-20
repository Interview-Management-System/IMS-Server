using InterviewManagementSystem.API.SignalR.Services;
using InterviewManagementSystem.Application.Managers.AuthenticationManager;
using InterviewManagementSystem.Application.Managers.InterviewManager;
using InterviewManagementSystem.Application.Managers.JobManager;
using InterviewManagementSystem.Application.Managers.UserManagers;
using InterviewManagementSystem.Infrastructure.Extensions;

namespace InterviewManagementSystem.API.Configurations;

internal static class InjectionService
{

    internal static void AddInjectionService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCloudinaryService(configuration);
        services.AddPostgreSqlDbContext(configuration);
        //services.AddHostedService<JobAutoCloserService>();
        //services.AddScoped<UserManager<Candidate>>();



        services.AddScoped<JobManager>();
        services.AddScoped<UserManager>();
        services.AddScoped<CandidateManager>();
        services.AddScoped<InterviewManager>();
        services.AddScoped<AuthenticationManager>();
        services.AddSingleton<UserHubService>();

    }
}





