using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Paginations;
using System.Linq.Expressions;

namespace InterviewManagementSystem.Application.Features.JobFeature.UseCases;

public sealed class JobRetrieveUseCase(IMapper mapper, IUnitOfWork unitOfWork) : BaseUseCase(mapper, unitOfWork)
{

    public async Task<ApiResponse<PageResult<JobForRetrieveDTO>>> GetListJobPagingAsync(JobPaginatedSearchRequest request)
    {

        PaginationParameter<Job> paginationParameter = _mapper.Map<PaginationParameter<Job>>(request);

        string[] includeProperties = [nameof(Job.Skills), nameof(Job.Levels), nameof(Job.JobStatus)];
        var pageResult = await _unitOfWork.JobRepository.GetPaginationList(paginationParameter, includeProperties);


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
            .SingleOrDefaultAsync();


        ArgumentNullException.ThrowIfNull(jobFoundById, "Job not found to view detail");


        return new ApiResponse<JobForRetrieveDTO>
        {
            Message = "Job detail found",
            Data = _mapper.Map<JobForRetrieveDTO>(jobFoundById)
        };
    }




    public async Task<ApiResponse<List<JobOpenForRetrieveDTO>>> GetListOpenJobAsync()
    {

        Expression<Func<Job, bool>>? filter = j => j.JobStatusId == JobStatusEnum.Open && j.IsDeleted == false;

        var listOpenJob = await _unitOfWork.JobRepository.GetAllAsync(filter);


        return new ApiResponse<List<JobOpenForRetrieveDTO>>
        {
            Message = listOpenJob.Count > 0 ? string.Empty : "No jobs found to display",
            Data = _mapper.Map<List<JobOpenForRetrieveDTO>>(listOpenJob)
        };
    }
}




