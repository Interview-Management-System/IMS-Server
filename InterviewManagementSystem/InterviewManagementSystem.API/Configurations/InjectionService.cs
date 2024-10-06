﻿using InterviewManagementSystem.Application.Features.AuthenticationFeatures;
using InterviewManagementSystem.Application.Features.AuthenticationFeatures.UseCases;
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

        //services.AddScoped<UserManager<Candidate>>();
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<InterviewManagementSystemContext>()
            .AddDefaultTokenProviders();


        services.AddScoped<IUnitOfWork, UnitOfWork>();

        AddJobServices(services);
        AddUserServices(services);
        AddOfferServices(services);
        AddAuthenticationServices(services);
    }






    private static void AddAuthenticationServices(IServiceCollection services)
    {
        services.AddScoped<LoginUseCase>();
        services.AddScoped<PasswordUseCase>();
        services.AddScoped<AuthenticationFacade>();
    }


    private static void AddUserServices(IServiceCollection services)
    {
        services.AddScoped<UserCreateUseCase>();
        services.AddScoped<UserStatusUseCase>();
        services.AddScoped<UserUpdateUseCase>();
        services.AddScoped<UserRetrieveUseCase>();
        services.AddScoped<CandidateStatusUseCase>();
        services.AddScoped<UserFacade>();
    }



    private static void AddJobServices(IServiceCollection services)
    {
        services.AddScoped<JobCreateUseCase>();
        services.AddScoped<JobStatusUseCase>();
        services.AddScoped<JobRetrieveUseCase>();
        services.AddScoped<JobUpdateUseCase>();
        //services.AddScoped<JobCreateUseCase>();
        services.AddScoped<JobFacade>();
    }


    private static void AddOfferServices(IServiceCollection services)
    {
        services.AddScoped<OfferRetrieveUseCase>();
        services.AddScoped<OfferCreateUseCase>();
        services.AddScoped<OfferFacade>();
    }
}





