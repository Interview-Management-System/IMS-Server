using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.Managers;
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

        var levelList = await MasterDataUtility.GetListLevelByIdList(jobForCreateDTO.LevelId);
        var benefitList = await MasterDataUtility.GetListBenefitByIdList(jobForCreateDTO.BenefitId);
        var skillList = await MasterDataUtility.GetListSkillByIdList(jobForCreateDTO.RequiredSkillId);


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
