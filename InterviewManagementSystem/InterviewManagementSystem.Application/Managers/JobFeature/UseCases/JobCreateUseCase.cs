using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Shared.EntityData.JobData;

namespace InterviewManagementSystem.Application.Managers.JobFeature.UseCases;

public sealed class JobCreateUseCase : BaseManager
{

    public JobCreateUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        MasterDataUtility.UnitOfWork = unitOfWork;
    }


    internal async Task<string> CreateNewJobAsync(JobForCreateDTO jobForCreateDTO)
    {

        var levelList = await MasterDataUtility.GetListLevelByIdListAsync(jobForCreateDTO.LevelId);
        var benefitList = await MasterDataUtility.GetListBenefitByIdListAsync(jobForCreateDTO.BenefitId);
        var skillList = await MasterDataUtility.GetListSkillByIdListAsync(jobForCreateDTO.RequiredSkillId);


        DataForCreateJob dataForCreateJob = _mapper.Map<DataForCreateJob>(jobForCreateDTO, opt =>
        {
            opt.Items[nameof(Job.Skills)] = skillList;
            opt.Items[nameof(Job.Levels)] = levelList;
            opt.Items[nameof(Job.Benefits)] = benefitList;
        });


        Job newJob = Job.Create(dataForCreateJob);


        await _unitOfWork.JobRepository.AddAsync(newJob);
        bool createdSuccessful = await _unitOfWork.SaveChangesAsync();


        ApplicationException.ThrowIfOperationFail(createdSuccessful, "Fail to create new job");
        return "New job created successfully";
    }
}
