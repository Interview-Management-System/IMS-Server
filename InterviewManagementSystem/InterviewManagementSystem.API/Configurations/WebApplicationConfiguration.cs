using AutoMapper;
using InterviewManagementSystem.Application.Shared.Helpers;
using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Interfaces;

namespace InterviewManagementSystem.API.Configurations;

internal static class WebApplicationConfiguration
{

    internal static void AddConfig(this WebApplication app)
    {

        var serviceProvider = app.Services;

        var mapper = serviceProvider.GetRequiredService<IMapper>();
        MapperHelper.InitializeMapperInstance(mapper);


        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var scopedServiceProvider = scope.ServiceProvider;
        var unitOfWork = scopedServiceProvider.GetRequiredService<IUnitOfWork>();
        MasterDataUtility.InitializeUnitOfWorkInstance(unitOfWork);
    }
}
