using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class MasterDataMappingProfile : Profile
{
    public MasterDataMappingProfile()
    {
        CreateMap(typeof(PaginationRequest), typeof(PaginationParameter<>)).ReverseMap();
    }

}
