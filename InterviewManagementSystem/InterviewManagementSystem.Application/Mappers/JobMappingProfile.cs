﻿using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.CustomClasses;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Paginations;

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
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.JobStatusId!.Value.GetJobStatusNameById()))
            .ForMember(dest => dest.RequiredSkills, opt => opt.MapFrom(src => src.Skills.Select(s => s.Name).ToList()))
            .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt!.Value.ToVieFormat()))
            .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => src.UpdateAt!.Value.ToVieFormat()))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.DatePeriod!.EndDate.ToVieFormat()))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.DatePeriod!.StartDate.ToVieFormat()))
            .ReverseMap();



        CreateMap<Job, JobForCreateDTO>().ReverseMap();
        CreateMap<PageResult<Job>, PageResult<JobForRetrieveDTO>>().ReverseMap();



        CreateMap<Job, JobForUpdateDTO>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.RequiredSkillId, opt => opt.Ignore())
             .ForMember(dest => dest.LevelId, opt => opt.Ignore())
             .ForMember(dest => dest.BenefitId, opt => opt.Ignore())
            .ReverseMap();



        CreateMap<JobForCreateDTO, JobMasterData>()
             .ForMember(dest => dest.LevelIdList, opt => opt.MapFrom(src => src.LevelId.Select(status => (short)status).ToArray()))
             .ForMember(dest => dest.BenefitIdList, opt => opt.MapFrom(src => src.BenefitId.Select(status => (short)status).ToArray()))
             .ForMember(dest => dest.SkillIdList, opt => opt.MapFrom(src => src.RequiredSkillId.Select(status => (short)status).ToArray()))
            .ReverseMap();
    }
}
