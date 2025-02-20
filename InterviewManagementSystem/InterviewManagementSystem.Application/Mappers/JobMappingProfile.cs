using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.Entities.Jobs;
using DatePeriod = InterviewManagementSystem.Application.DTOs.JobDTOs.DatePeriod;
using JobStatus = InterviewManagementSystem.Application.DTOs.JobDTOs.JobStatus;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class JobMappingProfile : Profile
{


    public JobMappingProfile()
    {
        CreateMap<Job, JobStatus>().ReverseMap();
        CreateMap<Domain.ValueObjects.DatePeriod, DatePeriod>().ReverseMap();
        CreateMap<Domain.ValueObjects.SalaryRange, SalaryRange>().ReverseMap();


        CreateMap<JobForUpdateDTO, Job>();
        CreateMap<JobForCreateDTO, Job>();


        #region Retrieve
        CreateMap<Job, JobForRetrieveDTO>()
             .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src))
             .ForMember(dest => dest.DatePeriod, opt => opt.MapFrom(src => src.DatePeriod))
             .ForMember(dest => dest.SalaryRange, opt => opt.MapFrom(src => src.SalaryRange))
             .ForMember(dest => dest.Levels, opt => opt.MapFrom(src => src.Levels.Select(l => l.Name)))
             .ForMember(dest => dest.RequiredSkills, opt => opt.MapFrom(src => src.Skills.Select(s => s.Name)))
            .IncludeAllDerived();


        CreateMap<Job, JobForDetailRetrieveDTO>()
            .ForMember(dest => dest.AuditInformation, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Benefits, opt => opt.MapFrom(src => src.Benefits.Select(b => b.Name)));

        CreateMap<Job, JobForPaginationRetrieveDTO>();
        CreateMap<PageResult<Job>, PageResult<JobForPaginationRetrieveDTO>>();
        #endregion
    }
}
