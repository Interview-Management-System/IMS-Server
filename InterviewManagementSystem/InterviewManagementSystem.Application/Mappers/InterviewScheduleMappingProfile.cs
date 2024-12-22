using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Domain.CustomClasses.EntityData.InterviewData;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Enums.Extensions;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class InterviewScheduleMappingProfile : Profile
{

    public InterviewScheduleMappingProfile()
    {
        CreateMap<PageResult<InterviewSchedule>, PageResult<InterviewScheduleForRetrieveDTO>>()
            .ReverseMap();


        // Base mapping for interview
        CreateMap<InterviewSchedule, BaseInterviewSchedule>()
            .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job!.Title))
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate!.UserName))
            .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.ScheduleTime!.Value.ToVieFormat()))
            .ForMember(dest => dest.Interviewers, opt => opt.MapFrom(src => src.Interviewers.Select(u => u.UserName).ToList()))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.InterviewResultId!.Value.GetEnumName()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.InterviewScheduleStatusId!.Value.GetEnumName()))
            .ReverseMap();


        CreateMap<InterviewSchedule, InterviewScheduleForRetrieveDTO>()
            .IncludeBase<InterviewSchedule, BaseInterviewSchedule>()
            .ForMember(dest => dest.EndHour, opt => opt.MapFrom(src => src.HourPeriod!.EndHour.ToNewFormat()))
            .ForMember(dest => dest.StartHour, opt => opt.MapFrom(src => src.HourPeriod!.StartHour.ToNewFormat()))
            .ReverseMap();


        CreateMap<InterviewSchedule, InterviewScheduleForDetailRetrieveDTO>()
            .IncludeBase<InterviewSchedule, BaseInterviewSchedule>()
            .ForMember(dest => dest.EndHour, opt => opt.MapFrom(src => src.HourPeriod!.EndHour.ToString()))
            .ForMember(dest => dest.StartHour, opt => opt.MapFrom(src => src.HourPeriod!.StartHour.ToString()))
            .ReverseMap();


        CreateMap<InterviewScheduleForCreateDTO, InterviewSchedule>()
            .ReverseMap();


        CreateMap<InterviewScheduleForCreateDTO, DataForCreateInterview>()
            .ForMember(dest => dest.StartHourString, opt => opt.MapFrom(src => src.StartHour))
            .ForMember(dest => dest.EndHourString, opt => opt.MapFrom(src => src.EndHour))
            .ReverseMap();


        CreateMap<InterviewScheduleForUpdateDTO, DataForUpdateInterview>()
            .ForMember(dest => dest.StartHourString, opt => opt.MapFrom(src => src.StartHour))
            .ForMember(dest => dest.EndHourString, opt => opt.MapFrom(src => src.EndHour))
            .ReverseMap();
    }
}
