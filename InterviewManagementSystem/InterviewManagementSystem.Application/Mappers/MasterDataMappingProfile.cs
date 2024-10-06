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


        CreateMap(typeof(PaginationRequest), typeof(PaginationParameter<>)).ReverseMap();
    }

}
