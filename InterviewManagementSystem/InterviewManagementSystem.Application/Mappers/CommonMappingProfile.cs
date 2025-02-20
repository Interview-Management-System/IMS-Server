using InterviewManagementSystem.Domain.Entities;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.Offers;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class CommonMappingProfile : Profile
{

    public CommonMappingProfile()
    {
        CreateMap<BaseEntity, AuditInformation>()
            .IncludeAllDerived()
            .ReverseMap();

        /*
        CreateMap<BaseEntity, PersonalInformation>()
            .IncludeAllDerived()
            .ReverseMap();

        CreateMap<BaseEntity, ProfessionalInformation>()
            .IncludeAllDerived()
            .ReverseMap();

        CreateMap<BaseEntity, UserStatus>()
            .IncludeAllDerived()
            .ReverseMap();
        */

        CreateMap<PaginatedSearchRequest, PaginationParameter<Job>>().MapPagination();
        CreateMap<PaginatedSearchRequest, PaginationParameter<AppUser>>().MapPagination();
        CreateMap<PaginatedSearchRequest, PaginationParameter<Candidate>>().MapPagination();
        CreateMap<PaginatedSearchRequest, PaginationParameter<Offer>>().MapPagination();
        CreateMap<PaginatedSearchRequest, PaginationParameter<InterviewSchedule>>().MapPagination();
    }
}