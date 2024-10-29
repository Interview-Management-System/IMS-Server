using InterviewManagementSystem.Application.CustomClasses.Utilities;
using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.CustomClasses.EntityData.JobData;
using InterviewManagementSystem.Domain.Entities.Jobs;

namespace InterviewManagementSystem.Application.Features.JobFeature.UseCases;

public sealed class JobCreateUseCase : BaseUseCase
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
