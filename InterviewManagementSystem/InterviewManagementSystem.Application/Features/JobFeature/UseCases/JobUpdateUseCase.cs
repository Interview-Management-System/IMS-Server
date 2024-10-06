using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.Aggregates;
using InterviewManagementSystem.Domain.CustomClasses;
using InterviewManagementSystem.Domain.Entities.Jobs;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Application.Features.JobFeature.UseCases;

public sealed class JobUpdateUseCase : BaseUseCase
{
    public JobUpdateUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }



    internal async Task<string> UpdateJobAsync(JobForUpdateDTO jobForUpdateDTO)
    {

        string[] includeProperties = [nameof(Job.Skills), nameof(Job.Levels), nameof(Job.Benefits)];


        var jobFoundById = await _unitOfWork.JobRepository
            .GetWithInclude(j => j.Id.Equals(jobForUpdateDTO.Id), includeProperties: includeProperties)
            .FirstOrDefaultAsync();


        ArgumentNullException.ThrowIfNull(jobFoundById, "Job not found to update");
        ApplicationException.ThrowIfGetDeletedRecord(jobFoundById.IsDeleted);



        JobMasterData jobMasterData = _mapper.Map<JobMasterData>(jobForUpdateDTO);


        var jobAggregate = new JobAggregate(jobFoundById, _unitOfWork);
        await jobAggregate.UpdateJobAsync(jobMasterData);



        _unitOfWork.JobRepository.Update(jobFoundById);


        bool updateSuccess = await _unitOfWork.SaveChangesAsync();
        ApplicationException.ThrowIfOperationFail(updateSuccess);

        return "Update successfully";
    }
}
