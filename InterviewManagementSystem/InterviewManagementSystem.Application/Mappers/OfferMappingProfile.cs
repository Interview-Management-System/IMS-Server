using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Domain.Entities.Offers;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class OfferMappingProfile : Profile
{
    public OfferMappingProfile()
    {


        CreateMap<Offer, OfferForRetrieveDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Candidate!.Email))
            .ForMember(dest => dest.Approver, opt => opt.MapFrom(src => src.Approver!.UserName))
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate!.UserName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OfferStatusId!.Value.GetOfferStatusNameById()))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.DepartmentId!.Value.GetDepartmentNameById()))
            .ReverseMap();



        // Load Interview then include user, candidate, approver.
        CreateMap<Offer, OfferForDetailRetrieveDTO>()
            .IncludeBase<Offer, OfferForRetrieveDTO>()
            .ForMember(dest => dest.InterviewNote, opt => opt.MapFrom(src => src.InterviewSchedule!.Note))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.LevelId!.Value.GetLevelNameById()))
            .ForMember(dest => dest.RecruiterOwner, opt => opt.MapFrom(src => src.RecruiterOwner!.UserName))
            .ForMember(dest => dest.InterviewTitle, opt => opt.MapFrom(src => src.InterviewSchedule!.Title))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.PositionId!.Value.GetPositionNameById()))
            .ForMember(dest => dest.ContractType, opt => opt.MapFrom(src => src.ContractTypeId!.Value.GetContractTypeNameById()))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate!.Value.ToString(DateUtility.VieDateFormatWithTime)))
            .ForMember(dest => dest.ContractTo, opt => opt.MapFrom(src => src.DatePeriod!.StartDate.ToString(DateUtility.VieDateFormatWithTime)))
            .ForMember(dest => dest.ContractFrom, opt => opt.MapFrom(src => src.DatePeriod!.EndDate.ToString(DateUtility.VieDateFormatWithTime)))
            .ForMember(dest => dest.Interviewers, opt => opt.MapFrom(src => src.InterviewSchedule!.AppUsers.Select(ap => ap.UserName).ToList()))
            .ReverseMap();



        CreateMap<Offer, OfferForCreateDTO>()
            .ForMember(dest => dest.ContractFrom, opt => opt.MapFrom(src => src.DatePeriod!.StartDate))
            .ForMember(dest => dest.ContractTo, opt => opt.MapFrom(src => src.DatePeriod!.EndDate))
            .ReverseMap();
    }
}
