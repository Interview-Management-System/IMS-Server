using InterviewManagementSystem.API.SignalR.Hubs;
using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace InterviewManagementSystem.API.Configurations;

internal static class WebApplicationConfiguration
{

    internal static void AddConfig(this WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        MasterDataUtility.InitializeUnitOfWorkInstance(unitOfWork);
    }



    internal static void UseHubs(this WebApplication webApplication)
    {
        webApplication.MapHub<UserHub>("/user-hub");
    }



    internal static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(x =>
        {
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                Type = SecuritySchemeType.Http
            });

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
            });

            x.MapType<FileResult>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            });
        });
    }
}
