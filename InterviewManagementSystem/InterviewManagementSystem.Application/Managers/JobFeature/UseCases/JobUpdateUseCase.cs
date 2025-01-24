using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.Managers;
using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Shared.EntityData.JobData;

namespace InterviewManagementSystem.Application.Managers.JobFeature.UseCases;

public sealed class JobUpdateUseCase : BaseManager
{


    public JobUpdateUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        MasterDataUtility.UnitOfWork = unitOfWork;
    }



    internal async Task<string> UpdateJobAsync(JobForUpdateDTO jobForUpdateDTO)
    {

        const string jobSkillString = nameof(Job.Skills);
        const string jobLevelString = nameof(Job.Levels);
        const string jobBenefitString = nameof(Job.Benefits);

        string[] includeProperties = [jobSkillString, jobLevelString, jobBenefitString];


        var jobFoundById = await _unitOfWork
            .GetBaseRepository<Job>()
            .GetWithInclude(j => j.Id.Equals(jobForUpdateDTO.Id), includeProperties: includeProperties, isTracking: true)
            .SingleOrDefaultAsync();

        ArgumentNullException.ThrowIfNull(jobFoundById, "Job not found to update");
        ApplicationException.ThrowIfGetDeletedRecord(jobFoundById.IsDeleted);



        var levelList = await MasterDataUtility.GetListLevelByIdList(jobForUpdateDTO.LevelId);
        var benefitList = await MasterDataUtility.GetListBenefitByIdList(jobForUpdateDTO.BenefitId);
        var skillList = await MasterDataUtility.GetListSkillByIdList(jobForUpdateDTO.RequiredSkillId);


        DataForUpdateJob dataForUpdateJob = _mapper.Map<DataForUpdateJob>(jobForUpdateDTO, opt =>
        {
            opt.Items[jobSkillString] = skillList;
            opt.Items[jobLevelString] = levelList;
            opt.Items[jobBenefitString] = benefitList;
        });


        Job.Update(jobFoundById, dataForUpdateJob);


        bool updateSuccess = await _unitOfWork.SaveChangesAsync();
        ApplicationException.ThrowIfOperationFail(updateSuccess);

        return "Update successfully";
    }
}
