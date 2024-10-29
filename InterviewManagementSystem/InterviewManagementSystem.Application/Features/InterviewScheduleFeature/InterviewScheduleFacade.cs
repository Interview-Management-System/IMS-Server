using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Application.Features.InterviewScheduleFeature.UseCases;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Features.InterviewScheduleFeature;

public sealed class InterviewScheduleFacade
{
    private readonly InterviewScheduleUpdateUseCase _interviewScheduleUpdateUseCase;
    private readonly InterviewScheduleCreateUseCase _interviewScheduleCreateUseCase;
    private readonly InterviewScheduleRetrieveUseCase _interviewScheduleRetrieveUseCase;

    public InterviewScheduleFacade
    (
        InterviewScheduleUpdateUseCase interviewScheduleUpdateUseCase,
        InterviewScheduleCreateUseCase interviewScheduleCreateUseCase,
        InterviewScheduleRetrieveUseCase interviewScheduleRetrieveUseCase
    )
    {
        _interviewScheduleUpdateUseCase = interviewScheduleUpdateUseCase;
        _interviewScheduleCreateUseCase = interviewScheduleCreateUseCase;
        _interviewScheduleRetrieveUseCase = interviewScheduleRetrieveUseCase;
    }



    public async Task<string> CreateInterviewScheduleAsync(InterviewScheduleForCreateDTO interviewScheduleForCreateDTO)
    {
        return await _interviewScheduleCreateUseCase.CreateInterviewScheduleAsync(interviewScheduleForCreateDTO);
    }



    public async Task<ApiResponse<PageResult<InterviewScheduleForRetrieveDTO>>> GetInterviewSchedulePagingAsync(PaginationRequest paginationRequest, Guid? interviewerId)
    {
        return await _interviewScheduleRetrieveUseCase.GetListInterviewPagingAsync(paginationRequest, interviewerId);
    }


    public async Task<ApiResponse<InterviewScheduleForDetailRetrieveDTO>> GetInterviewByIdAsync(Guid interviewId)
    {
        return await _interviewScheduleRetrieveUseCase.GetInterviewByIdAsync(interviewId);
    }


    public async Task<string> SetInterviewStatusAsync(Guid interviewId, InterviewStatusEnum interviewStatusId)
    {
        return await _interviewScheduleUpdateUseCase.ChangeInterviewStatusAsync(interviewId, interviewStatusId);
    }


    public async Task<string> SetInterviewResultAsync(Guid interviewId, InterviewResultEnum interviewResultId)
    {
        return await _interviewScheduleUpdateUseCase.ChangeInterviewResultAsync(interviewId, interviewResultId);
    }


    public async Task<string> UpdateInterviewAsync(InterviewScheduleForUpdateDTO interviewScheduleForUpdateDTO)
    {
        return await _interviewScheduleUpdateUseCase.UpdateAsync(interviewScheduleForUpdateDTO);
    }
}
