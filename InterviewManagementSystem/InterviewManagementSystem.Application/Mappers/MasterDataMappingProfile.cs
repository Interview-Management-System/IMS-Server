using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class MasterDataMappingProfile : Profile
{
    public MasterDataMappingProfile()
    {
        CreateMap<BenefitEnum, short>().ReverseMap();
        CreateMap<CandidateStatusEnum, short>().ReverseMap();
        CreateMap<ContractTypeEnum, short>().ReverseMap();
        CreateMap<DepartmentEnum, short>().ReverseMap();
        CreateMap<HighestLevelEnum, short>().ReverseMap();
        CreateMap<InterviewResultEnum, short>().ReverseMap();
        CreateMap<InterviewStatusEnum, short>().ReverseMap();
        CreateMap<JobStatusEnum, short>().ReverseMap();
        CreateMap<LevelEnum, short>().ReverseMap();
        CreateMap<OfferStatusEnum, short>().ReverseMap();
        CreateMap<PositionEnum, short>().ReverseMap();
        CreateMap<RoleEnum, short>().ReverseMap();
        CreateMap<SkillsEnum, short>().ReverseMap();


        CreateMap(typeof(PaginationRequest), typeof(PaginationParameter<>))
            .ReverseMap();
    }


    private static object CallGenericMethod(Type type)
    {
        // Define a generic method
        var method = typeof(MasterDataMappingProfile).GetMethod(nameof(GenericMethod), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        // Make it generic with the resolved type
        var genericMethod = method.MakeGenericMethod(type);

        // Invoke the generic method
        return genericMethod.Invoke(null, null);
    }

    private static T GenericMethod<T>()
    {
        // For demonstration purposes, just return a new instance of T
        return Activator.CreateInstance<T>();
    }
}
