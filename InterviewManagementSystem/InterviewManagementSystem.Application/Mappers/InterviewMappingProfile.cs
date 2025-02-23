using InterviewManagementSystem.Application.DTOs.InterviewDTOs;
using InterviewManagementSystem.Domain.Entities.Interviews;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class InterviewMappingProfile : Profile
{

    public InterviewMappingProfile()
    {

        CreateMap<Domain.ValueObjects.HourPeriod, HourPeriod>().ReverseMap();
        CreateMap<InterviewSchedule, DTOs.InterviewDTOs.InterviewResult>().ReverseMap();
        CreateMap<InterviewSchedule, InterviewStatus>().ReverseMap();


        CreateMap<InterviewSchedule, BaseInterviewDTO>()
            .ForMember(dest => dest.InterviewResult, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.InterviewStatus, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.HourPeriod, opt => opt.MapFrom(src => src.HourPeriod))
            .ReverseMap()
            .IncludeAllDerived();


        CreateMap<InterviewCreateDTO, InterviewSchedule>();


        #region Retrieve
        CreateMap<InterviewSchedule, InterviewRetrieveDTO>()
            .IncludeBase<InterviewSchedule, BaseInterviewDTO>()
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job!.Title))
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate!.UserName))
            .ForMember(dest => dest.Interviewers, opt => opt.MapFrom(src => src.Interviewers.Select(i => i.UserName)))
            .IncludeAllDerived();


        CreateMap<InterviewSchedule, InterviewForDetailRetrieveDTO>();
        CreateMap<InterviewSchedule, InterviewForPaginationRetrieveDTO>();
        CreateMap<PageResult<InterviewSchedule>, PageResult<InterviewForPaginationRetrieveDTO>>();
        #endregion



    }
}
