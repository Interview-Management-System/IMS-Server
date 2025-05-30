﻿using InterviewManagementSystem.Application.DTOs.InterviewDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;

namespace InterviewManagementSystem.Application.Managers.InterviewManager;

public sealed class InterviewManager(IUnitOfWork unitOfWork, UserManager<AppUser> userManager) : BaseManager<InterviewSchedule>(unitOfWork)
{


    public async Task<ApiResponse<PageResult<InterviewPaginationRetrieveDTO>>> GetListInterviewPagingAsync(InterviewPaginatedSearchRequest request)
    {

        PaginationParameter<InterviewSchedule> paginationParameter = MapperHelper.Map<PaginationParameter<InterviewSchedule>>(request);


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


        return await base.GetListPaginationAsync<InterviewPaginationRetrieveDTO>(paginationParameter);
    }



    public async Task<string> CreateInterview(InterviewCreateDTO interviewForCreateDTO)
    {

        var interview = MapperHelper.Map<InterviewSchedule>(interviewForCreateDTO);
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



    public async Task<string> SubmitInterviewAsync(InterviewSubmitResultDTO request)
    {

        var interview = await _repository.GetByIdAsync(request.Id, isTracking: true);
        ArgumentNullException.ThrowIfNull(interview, "Interview schedule not found");


        interview.SetResult(request.InterviewResultId);
        bool updatedSuccessful = await _unitOfWork.SaveChangesAsync();


        ApplicationException.ThrowIfOperationFail(updatedSuccessful, "Fail to submit result");
        return "Update successfully";
    }
}
