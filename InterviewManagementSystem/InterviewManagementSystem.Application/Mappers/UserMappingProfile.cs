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
        CreateMap<AppUser, BaseUserDTO>()
            .ForMember(dest => dest.PersonalInformation, opt => opt.MapFrom(src => src))
            .IncludeAllDerived();

        CreateMap<AppUser, UserStatus>().ReverseMap();
        CreateMap<AppUser, PersonalInformation>().ReverseMap();


        CreateMap<UserForCreateDTO, AppUser>()
              .IncludeMembers(src => src.PersonalInformation);


        #region Retrieve
        CreateMap<AppUser, UserForRetrieveDTO>()
            .ForMember(dest => dest.UserStatus, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles.FirstOrDefault()!.Name))
            .IncludeAllDerived();


        CreateMap<AppUser, UserForPaginationRetrieveDTO>()
            .ForMember(dest => dest.UserStatus, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles.FirstOrDefault()!.Name));


        CreateMap<AppUser, UserForDetailRetrieveDTO>();
        CreateMap<PageResult<AppUser>, PageResult<UserForPaginationRetrieveDTO>>();
        #endregion
    }



    private void CandidateMapping()
    {
        CreateMap<Candidate, ProfessionalInformation>();

        //CreateMap<Candidate, CandidateForRetrieveDTO>()
        //  .IncludeBase<AppUser, UserForRetrieveDTO>()

        //  ;
        //.ForMember(dest => dest.Department, opt => opt.Ignore())
        //.ForMember(dest => dest.DepartmentId, opt => opt.Ignore())
        //.ForMember(dest => dest.Offers, opt => opt.Ignore())
        //.ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => s.Name)))
        //.ForMember(dest => dest.RecruiterName, opt => opt.MapFrom(src => src.Recruiter!.UserName))
        //.ForMember(dest => dest.UpdateBy, opt => opt.MapFrom(src => src.UpdatedByNavigation!.UserName));


        CreateMap<CandidateForCreateDTO, Candidate>()
            .ForMember(dest => dest.Attachment, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.DepartmentId = null;
            })
            .ReverseMap();

        CreateMap<Candidate, CandidateForUpdateDTO>().ReverseMap();
        CreateMap<PageResult<Candidate>, PageResult<CandidateForUpdateDTO>>().ReverseMap();
    }
}
