
using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Domain.CustomClasses.EntityData.InterviewData;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Jobs;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Application.Features.InterviewScheduleFeature.UseCases;

public sealed class InterviewScheduleCreateUseCase : BaseUseCase
{

    private readonly UserManager<AppUser> _userManager;
    private readonly IBaseRepository<InterviewSchedule> _interviewScheduleRepository;


    public InterviewScheduleCreateUseCase(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager) : base(mapper, unitOfWork)
    {
        _userManager = userManager;
        _interviewScheduleRepository = _unitOfWork.GetBaseRepository<InterviewSchedule>();
    }



    internal async Task<string> CreateInterviewScheduleAsync(InterviewScheduleForCreateDTO interviewScheduleForCreateDTO)
    {

        Guid jobId = interviewScheduleForCreateDTO.JobId;
        Guid candidateId = interviewScheduleForCreateDTO.CandidateId;


        bool isCandidateInterviewed = await _interviewScheduleRepository
            .GetQuery()
            .AnyAsync(i => i.CandidateId == candidateId && i.JobId == jobId);

        // Create interview for the same candidate and same job is not allowed
        ApplicationException.ThrowIfInvalidOperation(isCandidateInterviewed, "Candidate was interviewed this job before");




        bool isCandidateExist = await _unitOfWork
            .GetBaseRepository<Candidate>()
            .GetQuery()
            .AnyAsync(i => i.Id == candidateId);

        ApplicationException.ThrowIfNoRecordFound(isCandidateExist, "Candidate not found to assign");




        bool isJobExist = await _unitOfWork
            .GetBaseRepository<Job>()
            .GetQuery()
            .AnyAsync(i => i.Id == jobId);

        ApplicationException.ThrowIfNoRecordFound(isJobExist, "Candidate not found to assign");




        // Get all available interviewers
        var interviewers = await _userManager.GetUsersInRoleAsync(RoleEnum.Interviewer.GetRoleName());
        var filteredInterviewers = interviewers.Where(i => interviewScheduleForCreateDTO.InterviewerList.Contains(i.Id)).ToList();

        ApplicationException.ThrowIfNoRecordFound((filteredInterviewers.Count == 0) is false, "No interviewers available");



        var dataForCreateInterview = _mapper.Map<DataForCreateInterview>(interviewScheduleForCreateDTO);
        dataForCreateInterview.Interviews = filteredInterviewers;


        var newInterviewSchedule = InterviewSchedule.Create(dataForCreateInterview);


        await _interviewScheduleRepository.AddAsync(newInterviewSchedule);
        bool createdSuccessful = await _unitOfWork.SaveChangesAsync();


        ApplicationException.ThrowIfOperationFail(createdSuccessful, "Fail to create new schedule");
        return "Create successfully";
    }
}
