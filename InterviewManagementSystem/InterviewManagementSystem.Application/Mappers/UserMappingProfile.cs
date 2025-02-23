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
        CreateMap<AppUser, UserIdentityRetrieveDTO>();

        CreateMap<AppUser, BaseUserDTO>()
            .ForMember(dest => dest.PersonalInformation, opt => opt.MapFrom(src => src))
            .IncludeAllDerived();

        CreateMap<AppUser, UserStatus>().ReverseMap();
        CreateMap<AppUser, PersonalInformation>().ReverseMap();


        CreateMap<UserCreateDTO, AppUser>()
              .IncludeMembers(src => src.PersonalInformation);


        #region Retrieve
        CreateMap<AppUser, UserRetrieveDTO>()
            .ForMember(dest => dest.UserStatus, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles.FirstOrDefault()!.Name))
            .IncludeAllDerived();


        CreateMap<AppUser, UserPaginationRetrieveDTO>()
            .ForMember(dest => dest.UserStatus, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles.FirstOrDefault()!.Name));


        CreateMap<AppUser, UserDetailRetrieveDTO>();
        CreateMap<PageResult<AppUser>, PageResult<UserPaginationRetrieveDTO>>();
        #endregion
    }





    private void CandidateMapping()
    {
        CreateMap<Candidate, UserIdentityRetrieveDTO>();

        CreateMap<Candidate, BaseCandidateDTO>()
            .ForMember(dest => dest.PersonalInformation, opt => opt.MapFrom(src => src))
            .IncludeAllDerived();

        CreateMap<Candidate, UserStatus>().ReverseMap();

        CreateMap<Candidate, AuditInformation>()
            .ForMember(dest => dest.UpdateBy, opt => opt.MapFrom(src => src.UpdatedByNavigation!.UserName))
            .ReverseMap();

        CreateMap<Candidate, PersonalInformation>().ReverseMap();
        CreateMap<Candidate, ProfessionalInformation>().ReverseMap();


        CreateMap<CandidateCreateDTO, Candidate>()
             .IncludeMembers(src => src.PersonalInformation)
             .IncludeMembers(src => src.ProfessionalInformation)
              .AfterMap((src, dest) =>
              {
                  dest.DepartmentId = null;
              });


        CreateMap<Candidate, CandidateUpdateDTO>().ReverseMap();



        #region Retrieve
        CreateMap<Candidate, CandidateRetrieveDTO>()
            .IncludeAllDerived();


        CreateMap<Candidate, CandidatePaginationRetrieveDTO>()
        .ForMember(dest => dest.UserStatus, opt => opt.MapFrom(src => src))
        .ForMember(dest => dest.OwnerHr, opt => opt.MapFrom(src => src.CreatedByNavigation!.UserName))
        .ForMember(dest => dest.CurrentPosition, opt => opt.MapFrom(src => src.PositionId.GetEnumName()))
        .ForMember(dest => dest.CandidateStatus, opt => opt.MapFrom(src => src.CandidateStatusId.GetValueOrDefault().GetEnumName()));


        CreateMap<Candidate, CandidateDetailRetrieveDTO>()
            .ForMember(dest => dest.Offers, opt => opt.Ignore())
            .ForMember(dest => dest.AuditInformation, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ProfessionalInformation, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => s.Name)))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => GenderHelper.GetGenderText(src.Gender)))
            ;

        CreateMap<PageResult<Candidate>, PageResult<CandidatePaginationRetrieveDTO>>();
        #endregion



    }
}
