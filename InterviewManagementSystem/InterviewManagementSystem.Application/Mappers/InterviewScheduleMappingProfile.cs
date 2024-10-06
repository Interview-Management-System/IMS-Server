using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class InterviewScheduleMappingProfile : Profile
{
    public InterviewScheduleMappingProfile()
    {
        CreateMap<PageResult<InterviewSchedule>, PageResult<InterviewScheduleForRetrieveDTO>>()
            .ReverseMap();


        CreateMap<InterviewSchedule, InterviewScheduleForRetrieveDTO>()
            .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src =>
                                                                        src.ScheduleTime!.Value.ToVieFormat()
                                                                        + " "
                                                                        + src.HourPeriod!.StartHour.ToNewFormat()
                                                                        + " - "
                                                                        + src.HourPeriod!.EndHour.ToNewFormat()))

            .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job!.Title))
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate!.UserName))
            .ForMember(dest => dest.Interviewers, opt => opt.MapFrom(src => src.Interviewers.Select(u => u.UserName).ToList()))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.InterviewResultId!.Value.GetInterviewResultNameById()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.InterviewScheduleStatusId!.Value.GetInterviewStatusNameById()))
            .ReverseMap();


        CreateMap<InterviewSchedule, InterviewScheduleForDetailRetrieveDTO>().ReverseMap();


        CreateMap<InterviewSchedule, InterviewScheduleForCreateDTO>()
             .ForMember(dest => dest.StartHour, opt => opt.MapFrom(src => src.HourPeriod!.StartHour))
             .ForMember(dest => dest.EndHour, opt => opt.MapFrom(src => src.HourPeriod!.EndHour))
            .ReverseMap();

    }
}
