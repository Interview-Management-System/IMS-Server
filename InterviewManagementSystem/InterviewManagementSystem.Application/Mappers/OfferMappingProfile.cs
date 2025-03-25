using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Domain.Shared.EntityData.OfferData;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class OfferMappingProfile : Profile
{


    public OfferMappingProfile()
    {


        CreateMap<Offer, OfferForRetrieveDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Candidate!.Email))
            .ForMember(dest => dest.Approver, opt => opt.MapFrom(src => src.Approver!.UserName))
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate!.UserName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OfferStatusId!.GetDescription()))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.DepartmentId!.GetDescription()))
            .ReverseMap();



        // Load Interview then include user, candidate, approver.
        CreateMap<Offer, OfferForDetailRetrieveDTO>()
            .IncludeBase<Offer, OfferForRetrieveDTO>()
            .ForMember(dest => dest.InterviewNote, opt => opt.MapFrom(src => src.InterviewSchedule!.Note))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.LevelId!.GetDescription()))
            .ForMember(dest => dest.RecruiterOwner, opt => opt.MapFrom(src => src.RecruiterOwner!.UserName))
            .ForMember(dest => dest.InterviewTitle, opt => opt.MapFrom(src => src.InterviewSchedule!.Title))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.PositionId!.GetDescription()))
            .ForMember(dest => dest.ContractType, opt => opt.MapFrom(src => src.ContractTypeId!.GetDescription()))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate!.Value.ToVieFormat()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OfferStatusId!.GetDescription()))
            .ForMember(dest => dest.ContractTo, opt => opt.MapFrom(src => src.DatePeriod!.StartDate.ToVieFormat()))
            .ForMember(dest => dest.ContractFrom, opt => opt.MapFrom(src => src.DatePeriod!.EndDate.ToVieFormat()))
            .ForMember(dest => dest.Interviewers, opt => opt.MapFrom(src => src.InterviewSchedule!.Interviewers.Select(ap => ap.UserName).ToList()))
            .ReverseMap();



        var createOfferMap = CreateMap<OfferForCreateDTO, DataForCreateOffer>()
            .ForPath(dest => dest.DatePeriod!.EndDate, opt => opt.MapFrom(src => src.ContractTo))
            .ForPath(dest => dest.DatePeriod!.StartDate, opt => opt.MapFrom(src => src.ContractFrom));



        var updateOfferMap = CreateMap<OfferForUpdateDTO, DataForUpdateOffer>()
            .ForPath(dest => dest.DatePeriod!.EndDate, opt => opt.MapFrom(src => src.ContractTo))
            .ForPath(dest => dest.DatePeriod!.StartDate, opt => opt.MapFrom(src => src.ContractFrom));


        CreateMap<PageResult<Offer>, PageResult<OfferForRetrieveDTO>>().ReverseMap();
    }

}
