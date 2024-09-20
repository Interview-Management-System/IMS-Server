using InterviewManagementSystem.Application.Mappers;

namespace InterviewManagementSystem.API.Configurations;

internal static class MapperConfiguration
{

    internal static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMappingProfile));
    }
}
