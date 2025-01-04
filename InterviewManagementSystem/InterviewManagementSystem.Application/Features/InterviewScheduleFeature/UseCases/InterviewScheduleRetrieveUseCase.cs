
using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Features.InterviewScheduleFeature.UseCases;

public sealed class InterviewScheduleRetrieveUseCase(IMapper mapper, IUnitOfWork unitOfWork) : BaseUseCase(mapper, unitOfWork)
{


    public async Task<ApiResponse<PageResult<InterviewScheduleForRetrieveDTO>>> GetListInterviewPagingAsync(InterviewSchedulePaginatedSearchRequest request)
    {

        PaginationParameter<InterviewSchedule> paginationParameter = _mapper.Map<PaginationParameter<InterviewSchedule>>(request);


        string[] includeProperties = [nameof(InterviewSchedule.Interviewers), nameof(InterviewSchedule.Job)];

        var pageResult = await _unitOfWork
            .InterviewScheduleRepository
            .GetPaginationList(paginationParameter, includeProperties);


        return new ApiResponse<PageResult<InterviewScheduleForRetrieveDTO>>
        {
            Message = pageResult.Items.Count > 0 ? "List interview found" : "No interviews found",
            Data = _mapper.Map<PageResult<InterviewScheduleForRetrieveDTO>>(pageResult)
        };
    }


    public async Task<ApiResponse<InterviewScheduleForDetailRetrieveDTO>> GetInterviewByIdAsync(Guid interviewerId)
    {

        string[] includeProperties =
            [
                nameof(InterviewSchedule.Candidate),
                nameof(InterviewSchedule.Job),
                nameof(InterviewSchedule.RecruiterOwner),
                nameof(InterviewSchedule.Interviewers)
            ];


        var interviewFoundById = await _unitOfWork
            .InterviewScheduleRepository
            .GetWithInclude(i => i.Id == interviewerId, includeProperties: includeProperties)
            .SingleOrDefaultAsync();


        ArgumentNullException.ThrowIfNull(interviewFoundById, "Schedule not found");
        ApplicationException.ThrowIfGetDeletedRecord(interviewFoundById.IsDeleted);


        var interviewDetail = _mapper.Map<InterviewScheduleForDetailRetrieveDTO>(interviewFoundById);


        return new ApiResponse<InterviewScheduleForDetailRetrieveDTO>
        {
            Data = interviewDetail,
            Message = "Interview schedule found",
        };
    }
}
