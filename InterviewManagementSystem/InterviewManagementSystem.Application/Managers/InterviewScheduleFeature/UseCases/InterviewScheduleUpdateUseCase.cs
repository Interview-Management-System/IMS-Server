using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Application.Managers;
using InterviewManagementSystem.Application.Shared.Exceptions;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Enums.Extensions;
using InterviewManagementSystem.Domain.Shared.EntityData.InterviewData;

namespace InterviewManagementSystem.Application.Managers.InterviewScheduleFeature.UseCases;

public sealed class InterviewScheduleUpdateUseCase : BaseManager
{

    private readonly UserManager<AppUser> _userManager;
    private readonly IBaseRepository<InterviewSchedule> _interviewRepository;


    public InterviewScheduleUpdateUseCase(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager) : base(mapper, unitOfWork)
    {
        _userManager = userManager;
        _interviewRepository = unitOfWork.GetBaseRepository<InterviewSchedule>();
    }



    internal async Task<string> UpdateAsync(InterviewScheduleForUpdateDTO interviewScheduleForUpdateDTO)
    {

        Guid jobId = interviewScheduleForUpdateDTO.JobId;
        Guid candidateId = interviewScheduleForUpdateDTO.CandidateId;


        string[] includeProperties = [nameof(InterviewSchedule.Interviewers)];

        var interviewFoundById = await _interviewRepository
            .GetWithInclude(i => i.Id == interviewScheduleForUpdateDTO.Id, isTracking: true, includeProperties: includeProperties)
            .SingleOrDefaultAsync();
        ArgumentNullException.ThrowIfNull(interviewFoundById, "Schedule not found");



        var interviewers = await _userManager.GetUsersInRoleAsync(RoleEnum.Interviewer.GetEnumName());
        var filteredInterviewers = interviewers.Where(i => interviewScheduleForUpdateDTO.InterviewerList.Contains(i.Id)).ToList();



        var dataForUpdateInterview = _mapper.Map<DataForUpdateInterview>(interviewScheduleForUpdateDTO);
        dataForUpdateInterview.Interviews = filteredInterviewers;



        InterviewSchedule.Update(interviewFoundById, dataForUpdateInterview);
        bool updateSuccess = await _unitOfWork.SaveChangesAsync();


        ApplicationException.ThrowIfOperationFail(updateSuccess, "Fail to update interview");

        return "Update successfully";
    }



    internal async Task<string> ChangeInterviewStatusAsync(Guid interviewId, InterviewStatusEnum interviewStatusId)
    {
        var interviewFoundById = await _interviewRepository.GetByIdAsync(interviewId, true);


        ArgumentNullException.ThrowIfNull(interviewFoundById, "Schedule not found");
        InterviewScheduleException.ThrowIfSetTheSameStatus(interviewFoundById, interviewStatusId);


        interviewFoundById.SetStatus(interviewStatusId);
        bool updateSuccess = await _unitOfWork.SaveChangesAsync();


        ApplicationException.ThrowIfOperationFail(updateSuccess, "Fail to set status");
        return "Change status successfully";
    }




    internal async Task<string> ChangeInterviewResultAsync(Guid interviewId, InterviewResultEnum interviewResultId)
    {

        var interviewFoundById = await _interviewRepository.GetByIdAsync(interviewId, true);

        ArgumentNullException.ThrowIfNull(interviewFoundById, "Schedule not found");
        InterviewScheduleException.ThrowIfSetTheSameResult(interviewFoundById, interviewResultId);


        // add to mongo for report
        if (interviewResultId == InterviewResultEnum.Pass)
        {
            interviewFoundById.MarkAsPassed();
        }
        else if (interviewResultId == InterviewResultEnum.Failed)
        {
            interviewFoundById.MarkAsFailed();
        }
        else
        {
            interviewFoundById.SetResult(interviewResultId);
        }


        bool updateSuccess = await _unitOfWork.SaveChangesAsync();


        ApplicationException.ThrowIfOperationFail(updateSuccess, "Fail to change result");
        return "Change result successfully";
    }
}
