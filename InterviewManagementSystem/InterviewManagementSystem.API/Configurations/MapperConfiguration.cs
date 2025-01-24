using InterviewManagementSystem.Application.Mappers;

namespace InterviewManagementSystem.API.Configurations;

internal static class MapperConfiguration
{

    internal static void AddMapper(this IServiceCollection services)
    {

        Type[] mappingTypes = [
            typeof(JobMappingProfile),
            typeof(UserMappingProfile),
            typeof(OfferMappingProfile),
            typeof(PaginationMappingProfile),
            typeof(InterviewMappingProfile),
        ];

        services.AddAutoMapper(mappingTypes);
    }
}
