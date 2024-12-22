using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Mappers;

public sealed class PaginationMappingProfile : Profile
{

    public PaginationMappingProfile()
    {
        JobPaginationMapping();
        UserPaginationMapping();
        OfferPaginationMapping();
        InterviewPaginationMapping();
    }


    private void OfferPaginationMapping()
    {

        CreateMap<PaginatedSearchRequest, PaginationParameter<Offer>>().MapPagination();

        CreateMap<OfferPaginatedSearchRequest, PaginationParameter<Offer>>()
           .AfterMap((src, dest) =>
           {
               dest.Filters.Add(o => o.DepartmentId == src.DepartmentId);
               dest.Filters.Add(o => o.OfferStatusId == src.OfferStatusId);
           });
    }


    private void JobPaginationMapping()
    {

        CreateMap<PaginatedSearchRequest, PaginationParameter<Job>>().MapPagination();

        CreateMap<JobPaginatedSearchRequest, PaginationParameter<Job>>()
           .AfterMap((src, dest) =>
           {
               dest.Filters.Add(o => o.JobStatusId == src.JobStatusId);
           });
    }


    private void InterviewPaginationMapping()
    {

        CreateMap<PaginatedSearchRequest, PaginationParameter<InterviewSchedule>>().MapPagination();

        CreateMap<InterviewSchedulePaginatedSearchRequest, PaginationParameter<InterviewSchedule>>()
           .AfterMap((src, dest) =>
           {
               dest.Filters.Add(o => o.InterviewScheduleStatusId == src.InterviewStatusId);
               dest.Filters.Add(i => src.InterviewerId == null || i.Interviewers.Any(interviewer => interviewer.Id == src.InterviewerId));
           });
    }


    private void UserPaginationMapping()
    {
        CreateMap<PaginatedSearchRequest, PaginationParameter<AppUser>>().MapPagination();
    }
}