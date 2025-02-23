using InterviewManagementSystem.Application.DTOs.InterviewDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;

namespace InterviewManagementSystem.Application.Managers.InterviewManager;

public sealed class InterviewManager(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager) : BaseManager<InterviewSchedule>(mapper, unitOfWork)
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



    public async Task<string> CreateInterview(InterviewCreateDTO interviewForCreateDTO)
    {

        var interview = _mapper.Map<InterviewSchedule>(interviewForCreateDTO);
        var interviewers = await userManager.GetUsersInRoleAsync(nameof(RoleEnum.Interviewer));

        var filteredInterviewers = interviewers
            .Where(interviewer => interviewForCreateDTO.InterviewerIds.Contains(interviewer.Id))
            .ToList();

        // Add relationship
        interview.AddInterviewers(filteredInterviewers);
        filteredInterviewers.ForEach(interviewer => interviewer.AddInterviewSchedule(interview));


        await _repository.AddAsync(interview);
        bool createdSuccessful = await _unitOfWork.SaveChangesAsync();

        ApplicationException.ThrowIfOperationFail(createdSuccessful, "Fail to create");
        return "Create successfully";

    }
}
