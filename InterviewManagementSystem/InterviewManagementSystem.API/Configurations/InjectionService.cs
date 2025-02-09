using InterviewManagementSystem.API.SignalR.Services;
using InterviewManagementSystem.Application.Managers.AuthenticationManager;
using InterviewManagementSystem.Application.Managers.InterviewScheduleFeature;
using InterviewManagementSystem.Application.Managers.InterviewScheduleFeature.UseCases;
using InterviewManagementSystem.Application.Managers.JobFeature;
using InterviewManagementSystem.Application.Managers.JobFeature.UseCases;
using InterviewManagementSystem.Application.Managers.OfferFeature;
using InterviewManagementSystem.Application.Managers.OfferFeature.UseCases;
using InterviewManagementSystem.Application.Managers.UserManagers;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Infrastructure.Persistences;
using InterviewManagementSystem.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.API.Configurations;

internal static class InjectionService
{

    internal static void AddInjectionService(this IServiceCollection services, IConfiguration configuration)
    {


        var connectionString = configuration["IMS_PostgreSqlSetting:ConnectionString"];
        ArgumentNullException.ThrowIfNullOrEmpty(connectionString, "Connection string not found");

        // Setup DB
        services.AddDbContext<InterviewManagementSystemContext>(options => options.UseNpgsql(connectionString));


        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<InterviewManagementSystemContext>()
            .AddDefaultTokenProviders();


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddHostedService<JobAutoCloserService>();

        AddJobServices(services);
        AddOfferServices(services);
        AddInterviewScheduleServices(services);


        //services.AddScoped<UserManager<Candidate>>();

        services.AddScoped<UserManager>();
        services.AddScoped<CandidateManager>();
        services.AddScoped<AuthenticationManager>();
        services.AddSingleton<UserHubService>();
    }



    private static void AddJobServices(IServiceCollection services)
    {
        services.AddScoped<JobFacade>();
        services.AddScoped<JobCreateUseCase>();
        services.AddScoped<JobStatusUseCase>();
        services.AddScoped<JobUpdateUseCase>();
        services.AddScoped<JobRetrieveUseCase>();
    }


    private static void AddOfferServices(IServiceCollection services)
    {
        services.AddScoped<OfferFacade>();
        services.AddScoped<OfferCreateUseCase>();
        services.AddScoped<OfferUpdateUseCase>();
        services.AddScoped<OfferRetrieveUseCase>();
    }



    private static void AddInterviewScheduleServices(IServiceCollection services)
    {
        services.AddScoped<InterviewScheduleFacade>();
        services.AddScoped<InterviewScheduleUpdateUseCase>();
        services.AddScoped<InterviewScheduleCreateUseCase>();
        services.AddScoped<InterviewScheduleRetrieveUseCase>();
    }
}





