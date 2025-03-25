using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Interfaces;

namespace InterviewManagementSystem.API.Configurations;

internal static class WebApplicationConfiguration
{

    internal static void AddConfig(this WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        MasterDataUtility.InitializeUnitOfWorkInstance(unitOfWork);
    }
}
