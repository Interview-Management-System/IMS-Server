using InterviewManagementSystem.Application.DTOs.UserDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Shared.Paginations;

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
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles.FirstOrDefault()!.Name));

        CreateMap<BaseUserDTO, AppUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


        CreateMap<UserForCreateDTO, AppUser>().ReverseMap();
        CreateMap<PageResult<AppUser>, PageResult<UserForRetrieveDTO>>().ReverseMap();
    }



    private void CandidateMapping()
    {
        CreateMap<Candidate, CandidateForRetrieveDTO>()
          .IncludeBase<AppUser, UserForRetrieveDTO>()
           .ForMember(dest => dest.Department, opt => opt.Ignore())
           .ForMember(dest => dest.DepartmentId, opt => opt.Ignore())
           .ForMember(dest => dest.Offers, opt => opt.Ignore())
           .ForMember(dest => dest.Skills, opt => opt.Ignore())
           .ForMember(dest => dest.RecruiterName, opt => opt.MapFrom(src => src.Recruiter!.UserName))
           //.ForMember(dest => dest.CV, opt => opt.MapFrom(src => FileUtility.CreateFileContentResultFromBytes(src.Attachment)))
           ;


        // aggre for skills
        CreateMap<CandidateForCreateDTO, Candidate>().ReverseMap();
        CreateMap<Candidate, CandidateForUpdateDTO>().ReverseMap();
    }
}
