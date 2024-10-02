using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Paginations;
using System.Linq.Expressions;

namespace InterviewManagementSystem.Application.Features.OfferFeature.UseCases;

public sealed class OfferRetrieveUseCase : BaseUseCase
{
    public OfferRetrieveUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }


    public async Task<ApiResponse<PageResult<JobForRetrieveDTO>>> GetListJobPagingAsync(string? jobTitle, JobStatusEnum? statusEnum, int pageSize, int pageIndex)
    {

        List<Expression<Func<Job, bool>>> listCondition = [j => j.Title!.Contains(jobTitle ?? "")];

        if (statusEnum.HasValue)
        {
            listCondition.Add(j => j.JobStatusId == (short)statusEnum);
        }


        PaginationParameter<Job> paginationParameter = new()
        {
            PageSize = pageSize,
            PageIndex = pageIndex,
            Filters = listCondition
        };


        string[] includeProperties = [nameof(Job.Skills), nameof(Job.Levels), nameof(Job.JobStatus)];
        var pageResult = await _unitOfWork.JobRepository.GetByPageWithIncludeAsync(paginationParameter, includeProperties);


        return new ApiResponse<PageResult<JobForRetrieveDTO>>
        {
            Message = pageResult.Items.Count > 0 ? "List job found" : "No jobs found",
            Data = _mapper.Map<PageResult<JobForRetrieveDTO>>(pageResult)
        };
    }
}
