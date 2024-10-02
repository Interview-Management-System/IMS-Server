using InterviewManagementSystem.Application.DTOs.UserDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        AppUserMapping();
        CandidateMapping();
    }


    private void AppUserMapping()
    {
        CreateMap<AppUser, UserForRetrieveDTO>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender! ? "Male" : "Female"))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsActive! ? "Active" : "In-Active"))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted!))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src =>
                                src.DepartmentId.HasValue ? src.DepartmentId.Value.GetDepartmentNameById() : null));



        CreateMap<BaseUserDTO, AppUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


    }



    private void CandidateMapping()
    {
        CreateMap<Candidate, CandidateForRetrieveDTO>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender! ? "Male" : "Female"))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsActive! ? "Active" : "In-Active"))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted!))
            .ForMember(dest => dest.RecruiterName, opt => opt.MapFrom(src => src.Recruiter!.UserName))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position!.Name))

            .ForMember(dest => dest.HighestLevel, opt => opt.MapFrom(src =>
                                src.HighestLevelId.HasValue ? src.HighestLevelId.Value.GetHighestLevelNameById() : null))

            .ForMember(dest => dest.CandidateStatus, opt => opt.MapFrom(src =>
                               src.CandidateStatusId.HasValue ? src.CandidateStatusId.Value.GetCandidateStatusNameById() : null))
            ;


        CreateMap<Candidate, CandidateForCreateDTO>().ReverseMap();
        CreateMap<Candidate, CandidateForUpdateDTO>().ReverseMap();
    }
}
