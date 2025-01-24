using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.Shared.Extensions;
using InterviewManagementSystem.Application.Shared.Helpers;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.MasterData;
using InterviewManagementSystem.Domain.Enums.Extensions;
using InterviewManagementSystem.Domain.Shared.EntityData.JobData;
using InterviewManagementSystem.Domain.Shared.Paginations;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class JobMappingProfile : Profile
{


    public JobMappingProfile()
    {


        CreateMap<Job, JobForRetrieveDTO>()
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedByNavigation!.UserName))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByNavigation!.UserName))
            .ForMember(dest => dest.Levels, opt => opt.MapFrom(src => src.Levels.Select(l => l.Name).ToList()))
            .ForMember(dest => dest.Benefits, opt => opt.MapFrom(src => src.Benefits.Select(l => l.Name).ToList()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.JobStatusId!.GetEnumName()))
            .ForMember(dest => dest.RequiredSkills, opt => opt.MapFrom(src => src.Skills.Select(s => s.Name).ToList()))
            .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt!.Value.ToVieFormat()))
            .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => src.UpdateAt!.Value.ToVieFormat()))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.DatePeriod!.EndDate.ToVieFormat()))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.DatePeriod!.StartDate.ToVieFormat()))
            .ReverseMap();




        CreateMap<Job, JobForUpdateDTO>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.RequiredSkillId, opt => opt.Ignore())
             .ForMember(dest => dest.LevelId, opt => opt.Ignore())
             .ForMember(dest => dest.BenefitId, opt => opt.Ignore())
            .ReverseMap();




        CreateMap<JobForCreateDTO, DataForCreateJob>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom((src, dest, destMember, context)
                    => MapperHelper.GetListFromContext<Skill>(context, nameof(Job.Skills))))

            .ForMember(dest => dest.Benefits, opt => opt.MapFrom((src, dest, destMember, context)
                    => MapperHelper.GetListFromContext<Benefit>(context, nameof(Job.Benefits))))

            .ForMember(dest => dest.Levels, opt => opt.MapFrom((src, dest, destMember, context)
                    => MapperHelper.GetListFromContext<Level>(context, nameof(Job.Levels))))
            .ReverseMap();



        CreateMap<JobForUpdateDTO, DataForUpdateJob>()
          .ForMember(dest => dest.Skills, opt => opt.MapFrom((src, dest, destMember, context)
                    => MapperHelper.GetListFromContext<Skill>(context, nameof(Job.Skills))))

          .ForMember(dest => dest.Benefits, opt => opt.MapFrom((src, dest, destMember, context)
                    => MapperHelper.GetListFromContext<Benefit>(context, nameof(Job.Benefits))))

          .ForMember(dest => dest.Levels, opt => opt.MapFrom((src, dest, destMember, context)
                    => MapperHelper.GetListFromContext<Level>(context, nameof(Job.Levels))))
          .ReverseMap();




        CreateMap<PageResult<Job>, PageResult<JobForRetrieveDTO>>().ReverseMap();
    }
}
