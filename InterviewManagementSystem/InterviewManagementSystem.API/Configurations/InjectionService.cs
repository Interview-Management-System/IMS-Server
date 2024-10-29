using InterviewManagementSystem.Application.CustomClasses.BackgroundServices;
using InterviewManagementSystem.Application.Features.AuthenticationFeatures;
using InterviewManagementSystem.Application.Features.AuthenticationFeatures.UseCases;
using InterviewManagementSystem.Application.Features.InterviewScheduleFeature;
using InterviewManagementSystem.Application.Features.InterviewScheduleFeature.UseCases;
using InterviewManagementSystem.Application.Features.JobFeature;
using InterviewManagementSystem.Application.Features.JobFeature.UseCases;
using InterviewManagementSystem.Application.Features.OfferFeature;
using InterviewManagementSystem.Application.Features.OfferFeature.UseCases;
using InterviewManagementSystem.Application.Features.UserFeature;
using InterviewManagementSystem.Application.Features.UserFeature.UseCases;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Interfaces;
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


        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<InterviewManagementSystemContext>()
            .AddDefaultTokenProviders();


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHostedService<JobAutoCloserService>();

        AddJobServices(services);
        AddUserServices(services);
        AddOfferServices(services);
        AddAuthenticationServices(services);
        AddInterviewScheduleServices(services);
    }






    private static void AddAuthenticationServices(IServiceCollection services)
    {
        services.AddScoped<LoginUseCase>();
        services.AddScoped<PasswordUseCase>();
        services.AddScoped<AuthenticationFacade>();
    }



    private static void AddUserServices(IServiceCollection services)
    {
        services.AddScoped<UserFacade>();
        services.AddScoped<UserCreateUseCase>();
        services.AddScoped<UserStatusUseCase>();
        services.AddScoped<UserUpdateUseCase>();
        services.AddScoped<UserRetrieveUseCase>();
        services.AddScoped<CandidateStatusUseCase>();
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





