using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Domain.CustomClasses.EntityData.OfferData;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Domain.Enums.Extensions;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class OfferMappingProfile : Profile
{


    public OfferMappingProfile()
    {


        CreateMap<Offer, OfferForRetrieveDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Candidate!.Email))
            .ForMember(dest => dest.Approver, opt => opt.MapFrom(src => src.Approver!.UserName))
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate!.UserName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OfferStatusId!.GetEnumName()))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.DepartmentId!.GetEnumName()))
            .ReverseMap();



        // Load Interview then include user, candidate, approver.
        CreateMap<Offer, OfferForDetailRetrieveDTO>()
            .IncludeBase<Offer, OfferForRetrieveDTO>()
            .ForMember(dest => dest.InterviewNote, opt => opt.MapFrom(src => src.InterviewSchedule!.Note))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.LevelId!.GetEnumName()))
            .ForMember(dest => dest.RecruiterOwner, opt => opt.MapFrom(src => src.RecruiterOwner!.UserName))
            .ForMember(dest => dest.InterviewTitle, opt => opt.MapFrom(src => src.InterviewSchedule!.Title))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.PositionId!.GetEnumName()))
            .ForMember(dest => dest.ContractType, opt => opt.MapFrom(src => src.ContractTypeId!.GetEnumName()))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate!.Value.ToVieFormat()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OfferStatusId!.GetEnumName()))
            .ForMember(dest => dest.ContractTo, opt => opt.MapFrom(src => src.DatePeriod!.StartDate.ToVieFormat()))
            .ForMember(dest => dest.ContractFrom, opt => opt.MapFrom(src => src.DatePeriod!.EndDate.ToVieFormat()))
            .ForMember(dest => dest.Interviewers, opt => opt.MapFrom(src => src.InterviewSchedule!.Interviewers.Select(ap => ap.UserName).ToList()))
            .ReverseMap();



        var createOfferMap = CreateMap<OfferForCreateDTO, DataForCreateOffer>()
        .ForPath(dest => dest.DatePeriod!.EndDate, opt => opt.MapFrom(src => src.ContractTo))
        .ForPath(dest => dest.DatePeriod!.StartDate, opt => opt.MapFrom(src => src.ContractFrom));

        CreateFlexibleMap(createOfferMap);



        var updateOfferMap = CreateMap<OfferForUpdateDTO, DataForUpdateOffer>()
        .ForPath(dest => dest.DatePeriod!.EndDate, opt => opt.MapFrom(src => src.ContractTo))
        .ForPath(dest => dest.DatePeriod!.StartDate, opt => opt.MapFrom(src => src.ContractFrom));

        CreateFlexibleMap(updateOfferMap);


        CreateMap<PageResult<Offer>, PageResult<OfferForRetrieveDTO>>().ReverseMap();


        /*
        CreateMap<OfferForCreateDTO, DataForCreateOffer>()
            .ForMember(dest => dest.LevelId, opt => opt.MapFrom(src => (short)src.LevelId))
            .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => (short)src.PositionId))
            .ForPath(dest => dest.DatePeriod!.EndDate, opt => opt.MapFrom(src => src.ContractTo))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => (short)src.DepartmentId))
            .ForPath(dest => dest.DatePeriod!.StartDate, opt => opt.MapFrom(src => src.ContractFrom))
            .ForMember(dest => dest.ContractTypeId, opt => opt.MapFrom(src => (short)src.ContractTypeId))
            .ForMember(dest => dest.AssociatedCandidate, opt => opt.MapFrom((src, dest, destMember, context) => MappingHelper.GetContextItem<Candidate>(context, "Candidate")))
            .ForMember(dest => dest.AssociatedInterviewSchedule, opt => opt.MapFrom((src, dest, destMember, context) => MappingHelper.GetContextItem<InterviewSchedule>(context, "InterviewSchedule")))
            .ReverseMap();

        CreateMap<OfferForUpdateDTO, DataForUpdateOffer>()
             .ForMember(dest => dest.LevelId, opt => opt.MapFrom(src => (short)src.LevelId))
            .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => (short)src.PositionId))
            .ForPath(dest => dest.DatePeriod!.EndDate, opt => opt.MapFrom(src => src.ContractTo))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => (short)src.DepartmentId))
            .ForPath(dest => dest.DatePeriod!.StartDate, opt => opt.MapFrom(src => src.ContractFrom))
            .ForMember(dest => dest.ContractTypeId, opt => opt.MapFrom(src => (short)src.ContractTypeId))
            .ForMember(dest => dest.AssociatedCandidate, opt => opt.MapFrom((src, dest, destMember, context) => MappingHelper.GetContextItem<Candidate>(context, "Candidate")))
            .ForMember(dest => dest.AssociatedInterviewSchedule, opt => opt.MapFrom((src, dest, destMember, context) => MappingHelper.GetContextItem<InterviewSchedule>(context, "InterviewSchedule")))
            .ReverseMap();
        */
    }





    private static void CreateFlexibleMap<TSource, TDestination>(IMappingExpression<TSource, TDestination> mappingExpression)
    {


        const string levelIdString = nameof(BaseOfferData.LevelId);
        const string positionIdString = nameof(BaseOfferData.PositionId);
        const string contractTypeIdString = nameof(BaseOfferData.ContractTypeId);
        const string associatedCandidate = nameof(BaseOfferData.AssociatedCandidate);
        const string associatedInterviewSchedule = nameof(BaseOfferData.AssociatedInterviewSchedule);


        // Dynamically set properties using inline lambda if properties exist
        AddConditionalMapping(mappingExpression, levelIdString, levelIdString, src => (short)GetPropertyValue(src, levelIdString)!);
        AddConditionalMapping(mappingExpression, positionIdString, positionIdString, src => (short)GetPropertyValue(src, positionIdString)!);
        AddConditionalMapping(mappingExpression, contractTypeIdString, contractTypeIdString, src => (short)GetPropertyValue(src, contractTypeIdString)!);


        // Context item mappings for complex properties (if present)
        if (PropertyExists<TDestination>(associatedCandidate))
        {
            mappingExpression.ForMember(associatedCandidate, opt => opt.MapFrom((src, dest, _, context)
                => MappingHelper.GetContextItem<Candidate>(context, nameof(Candidate))));
        }


        if (PropertyExists<TDestination>(associatedInterviewSchedule))
        {
            mappingExpression.ForMember(associatedInterviewSchedule, opt => opt.MapFrom((src, dest, _, context)
                => MappingHelper.GetContextItem<InterviewSchedule>(context, nameof(InterviewSchedule))));
        }
    }


    private static void AddConditionalMapping<TSource, TDestination, TProp>(IMappingExpression<TSource, TDestination> map, string srcPropName, string destPropName, Func<TSource, TProp> mapFunc)
    {
        if (PropertyExists<TSource>(srcPropName) && PropertyExists<TDestination>(destPropName))
        {
            map.ForMember(destPropName, opt => opt.MapFrom(src => mapFunc(src)));
        }
    }


    private static object? GetPropertyValue<T>(T obj, string propertyName)
    {
        return typeof(T).GetProperty(propertyName)?.GetValue(obj);
    }


    private static bool PropertyExists<T>(string propName)
    {
        return typeof(T).GetProperty(propName) != null;
    }


}
