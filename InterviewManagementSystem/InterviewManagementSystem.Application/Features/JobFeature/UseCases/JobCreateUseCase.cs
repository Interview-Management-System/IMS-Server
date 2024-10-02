
using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.Aggregates;
using InterviewManagementSystem.Domain.CustomClasses;
using InterviewManagementSystem.Domain.Entities.Jobs;

namespace InterviewManagementSystem.Application.Features.JobFeature.UseCases;

public sealed class JobCreateUseCase : BaseUseCase
{

    public JobCreateUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {

    }


    internal async Task<string> CreateNewJobAsync(JobForCreateDTO jobForCreateDTO)
    {
        var mappedJob = _mapper.Map<Job>(jobForCreateDTO);
        JobMasterData jobMasterData = _mapper.Map<JobMasterData>(jobForCreateDTO);


        var jobAggregate = new JobAggregate(mappedJob, _unitOfWork);
        var newJob = await jobAggregate.CreateJobAsync(jobMasterData);


        await _unitOfWork.JobRepository.AddAsync(newJob);
        bool createdSuccessful = await _unitOfWork.SaveChangesAsync();


        ApplicationException.ThrowIfOperationFail(createdSuccessful, "Fail to create new job");
        return "New job created successfully";
    }
}
