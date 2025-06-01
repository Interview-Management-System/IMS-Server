using InterviewManagementSystem.API.SignalR.Notifiers;
using InterviewManagementSystem.Application.Managers.AuthenticationManager;
using InterviewManagementSystem.Application.Managers.InterviewManager;
using InterviewManagementSystem.Application.Managers.JobManager;
using InterviewManagementSystem.Application.Managers.UserManagers;
using InterviewManagementSystem.Application.Mappers;
using InterviewManagementSystem.Application.Shared.Helpers;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Infrastructure.Extensions;
using InterviewManagementSystem.Infrastructure.UnitOfWorks;

namespace InterviewManagementSystem.API.Configurations;

internal static class InjectionService
{

    internal static void AddInjectionService(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddCloudinaryService(configuration);
        services.AddPostgreSqlDbContext(configuration, env);
        //services.AddHostedService<JobAutoCloserService>();
        //services.AddScoped<UserManager<Candidate>>();


        services.AddScoped<JobManager>();
        services.AddScoped<UserManager>();
        services.AddScoped<CandidateManager>();
        services.AddScoped<InterviewManager>();
        services.AddScoped<AuthenticationManager>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<UserHubNotifier>();
        services.AddSingleton<TokenHelper>();
    }


    internal static void AddMapper(this IServiceCollection services)
    {
        var config = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile<JobMappingProfile>();
            cfg.AddProfile<UserMappingProfile>();
            cfg.AddProfile<OfferMappingProfile>();
            cfg.AddProfile<CommonMappingProfile>();
            cfg.AddProfile<InterviewMappingProfile>();
        });

        /*
        services.AddAutoMapper(typeof(JobMappingProfile),
                               typeof(UserMappingProfile),
                               typeof(OfferMappingProfile),
                               typeof(CommonMappingProfile),
                               typeof(InterviewMappingProfile));
        */
        MapperHelper.InitMapper(config.CreateMapper());
    }
}





