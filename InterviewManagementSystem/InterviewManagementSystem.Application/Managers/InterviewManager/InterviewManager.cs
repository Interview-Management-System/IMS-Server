using InterviewManagementSystem.Application.DTOs.InterviewDTOs;
using InterviewManagementSystem.Domain.Entities.Interviews;

namespace InterviewManagementSystem.Application.Managers.InterviewManager;

public sealed class InterviewManager(IMapper mapper, IUnitOfWork unitOfWork) : BaseManager<InterviewSchedule>(mapper, unitOfWork)
{


    public async Task<ApiResponse<PageResult<InterviewForPaginationRetrieveDTO>>> GetListInterviewPagingAsync(InterviewPaginatedSearchRequest request)
    {

        PaginationParameter<InterviewSchedule> paginationParameter = _mapper.Map<PaginationParameter<InterviewSchedule>>(request);


        InterviewStatusEnum statusId = request.InterviewStatusId;
        if (statusId.IsNotDefault())
        {
            paginationParameter.Filters.Add(i => i.InterviewScheduleStatusId == statusId);
        }


        Guid? interviewerId = request.InterviewerId;
        if (interviewerId != null)
        {
            paginationParameter.Filters.Add(i => i.Interviewers.Select(a => a.Id).Equals(interviewerId));
        }


        return await base.GetListPaginationAsync<InterviewForPaginationRetrieveDTO>(paginationParameter);
    }


    public void CreateInterview()
    {

    }
}
