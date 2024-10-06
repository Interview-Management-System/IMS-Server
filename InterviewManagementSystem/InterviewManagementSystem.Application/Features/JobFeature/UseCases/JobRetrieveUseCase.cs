using InterviewManagementSystem.Application.CustomClasses.Helpers;
using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Paginations;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Application.Features.JobFeature.UseCases;

public sealed class JobRetrieveUseCase : BaseUseCase
{


    public JobRetrieveUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }



    public async Task<ApiResponse<PageResult<JobForRetrieveDTO>>> GetListJobPagingAsync(PaginationRequest paginationRequest)
    {

        var filters = FilterHelper.BuildFilters<Job>(paginationRequest, nameof(Job.Title));


        PaginationParameter<Job> paginationParameter = _mapper.Map<PaginationParameter<Job>>(paginationRequest);
        paginationParameter.Filters = filters;


        string[] includeProperties = [nameof(Job.Skills), nameof(Job.Levels), nameof(Job.JobStatus)];
        var pageResult = await _unitOfWork.JobRepository.GetByPageWithIncludeAsync(paginationParameter, includeProperties);


        return new ApiResponse<PageResult<JobForRetrieveDTO>>
        {
            Message = pageResult.Items.Count > 0 ? "List job found" : "No jobs found",
            Data = _mapper.Map<PageResult<JobForRetrieveDTO>>(pageResult)
        };
    }



    public async Task<ApiResponse<JobForRetrieveDTO>> GetJobDetailByIdAsync(Guid id)
    {

        string[] includeProperties = [
                nameof(Job.Skills),
                nameof(Job.Levels),
                nameof(Job.Benefits),
                nameof(Job.CreatedByNavigation),
                nameof(Job.UpdatedByNavigation)
            ];


        var jobFoundById = await _unitOfWork.JobRepository
            .GetWithInclude(j => j.Id.Equals(id), includeProperties: includeProperties)
            .FirstOrDefaultAsync();


        ArgumentNullException.ThrowIfNull(jobFoundById, "Job not found to view detail");


        return new ApiResponse<JobForRetrieveDTO>
        {
            Message = "Job detail found",
            Data = _mapper.Map<JobForRetrieveDTO>(jobFoundById)
        };
    }
}




