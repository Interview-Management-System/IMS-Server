
using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Entities.Jobs;

namespace InterviewManagementSystem.Application.Managers.JobManager;

public sealed class JobManager : BaseManager<Job>
{

    public JobManager(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork) { }


    public async Task<List<JobOpenRetrieveDTO>> GetListOpenJobAsync()
    {
        var projection = MapperHelper.CreateProjection<Job, JobOpenRetrieveDTO>(_mapper);
        return await _repository.GetAllAsync(f => f.JobStatusId == JobStatusEnum.Open, projection);
    }


    public async Task<ApiResponse<PageResult<JobPaginationRetrieveDTO>>> GetListJobPagingAsync(JobPaginatedSearchRequest request)
    {

        PaginationParameter<Job> paginationParameter = _mapper.Map<PaginationParameter<Job>>(request);

        JobStatusEnum statusId = request.JobStatusId;

        if (statusId.IsNotDefault())
        {
            paginationParameter.Filters.Add(j => j.JobStatusId == statusId);
        }

        return await base.GetListPaginationAsync<JobPaginationRetrieveDTO>(paginationParameter);
    }


    public async Task<string> CreateNewJobAsync(JobCreateDTO jobForCreateDTO)
    {

        Job job = _mapper.Map<Job>(jobForCreateDTO);

        var levels = await MasterDataUtility.GetListLevelByIdListAsync(jobForCreateDTO.LevelIds);
        var skills = await MasterDataUtility.GetListSkillByIdListAsync(jobForCreateDTO.SkillIds);
        var benefits = await MasterDataUtility.GetListBenefitByIdListAsync(jobForCreateDTO.BenefitIds);

        job.AddSkills(skills);
        job.AddLevels(levels);
        job.AddBenefits(benefits);

        await _repository.AddAsync(job);
        bool createdSuccessful = await _unitOfWork.SaveChangesAsync();

        ApplicationException.ThrowIfOperationFail(createdSuccessful, "Fail to create new job");

        return "New job created successfully";
    }



    public async Task<string> UpdateJobAsync(JobUpdateDTO jobForUpdateDTO)
    {

        string[] includeProperties =
        [
            nameof(Job.Skills),
            nameof(Job.Levels),
            nameof(Job.Benefits)
        ];

        var jobFoundById = await _repository
          .GetWithInclude(j => j.Id.Equals(jobForUpdateDTO.Id), includeProperties: includeProperties, isTracking: true)
          .SingleOrDefaultAsync();

        ArgumentNullException.ThrowIfNull(jobFoundById);


        _mapper.Map(jobForUpdateDTO, jobFoundById);

        /*
        var levels = await MasterDataUtility.GetListLevelByIdListAsync(jobForUpdateDTO.LevelIds, _unitOfWork);
        var skills = await MasterDataUtility.GetListSkillByIdListAsync(jobForUpdateDTO.SkillIds, _unitOfWork);
        var benefits = await MasterDataUtility.GetListBenefitByIdListAsync(jobForUpdateDTO.BenefitIds, _unitOfWork);


        jobFoundById.AddSkills(skills);
        jobFoundById.AddLevels(levels);
        jobFoundById.AddBenefits(benefits);
        */

        _repository.Update(jobFoundById);
        bool updatedSuccessful = await _unitOfWork.SaveChangesAsync();

        ApplicationException.ThrowIfOperationFail(updatedSuccessful, "Fail to create new job");

        return "Update job successfully";
    }
}



