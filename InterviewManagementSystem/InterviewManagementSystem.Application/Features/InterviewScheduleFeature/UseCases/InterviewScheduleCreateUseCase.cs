
using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Domain.Entities.Interviews;

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




        return "Create successfully";
    }
}
