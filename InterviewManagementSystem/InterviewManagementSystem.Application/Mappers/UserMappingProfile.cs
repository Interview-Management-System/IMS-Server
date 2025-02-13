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
        CreateMap<Candidate, BaseCandidateDTO>()
        .ForMember(dest => dest.PersonalInformation, opt => opt.MapFrom(src => src))
        .IncludeAllDerived();

        CreateMap<Candidate, UserStatus>().ReverseMap();

        CreateMap<Candidate, AuditInformation>()
            .ForMember(dest => dest.UpdateBy, opt => opt.MapFrom(src => src.UpdatedByNavigation!.UserName))
            .ReverseMap();

        CreateMap<Candidate, PersonalInformation>().ReverseMap();
        CreateMap<Candidate, ProfessionalInformation>().ReverseMap();


        CreateMap<CandidateForCreateDTO, Candidate>()
             .IncludeMembers(src => src.PersonalInformation)
             .IncludeMembers(src => src.ProfessionalInformation)
              .AfterMap((src, dest) =>
               {
                   dest.DepartmentId = null;
               });


        CreateMap<Candidate, CandidateForUpdateDTO>().ReverseMap();



        #region Retrieve
        CreateMap<Candidate, CandidateForRetrieveDTO>()
            .IncludeAllDerived();


        CreateMap<Candidate, CandidateForPaginationRetrieveDTO>()
        .ForMember(dest => dest.UserStatus, opt => opt.MapFrom(src => src))
        .ForMember(dest => dest.OwnerHr, opt => opt.MapFrom(src => src.CreatedByNavigation!.UserName))
        .ForMember(dest => dest.CurrentPosition, opt => opt.MapFrom(src => src.PositionId.GetEnumName()))
        .ForMember(dest => dest.CandidateStatus, opt => opt.MapFrom(src => src.CandidateStatusId.GetValueOrDefault().GetEnumName()));


        CreateMap<Candidate, CandidateForDetailRetrieveDTO>()
            .ForMember(dest => dest.Offers, opt => opt.Ignore())
            .ForMember(dest => dest.AuditInformation, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ProfessionalInformation, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => s.Name)))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => GenderHelper.GetGenderText(src.Gender)))
            ;


        CreateMap<PageResult<Candidate>, PageResult<CandidateForPaginationRetrieveDTO>>();
        #endregion



    }
}
