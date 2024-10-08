
using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.ValueObjects;

namespace InterviewManagementSystem.Application.Features.InterviewScheduleFeature.UseCases;

public sealed class InterviewScheduleCreateUseCase : BaseUseCase
{


    public InterviewScheduleCreateUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }



    public async Task<string> CreateInterviewScheduleAsync(InterviewScheduleForCreateDTO interviewScheduleForCreateDTO)
    {

        // create need add job id
        var interviewSchedule = _mapper.Map<InterviewSchedule>(interviewScheduleForCreateDTO);


        string startHourString = interviewScheduleForCreateDTO.StartHour!;
        string endHourString = interviewScheduleForCreateDTO.EndHour!;
        interviewSchedule.HourPeriod = HourPeriod.CreatePeriod(startHourString, endHourString);



        var us = await _unitOfWork
            .AppUserRepository
            .GetAllAsync(u => interviewScheduleForCreateDTO.InterviewerList.Contains(u.Id));

        interviewSchedule.ClearAllInterviewers();
        interviewSchedule.SetInterviewers(us);

        await _unitOfWork.InterviewScheduleRepository.AddAsync(interviewSchedule);
        bool createdSuccessful = await _unitOfWork.SaveChangesAsync();


        ApplicationException.ThrowIfOperationFail(createdSuccessful, "Fail to create new schedule");
        return "Create successfully";
    }
}
