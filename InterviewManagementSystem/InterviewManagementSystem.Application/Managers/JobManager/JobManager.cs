
using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.Entities.Jobs;

namespace InterviewManagementSystem.Application.Managers.JobManager;

public sealed class JobManager(IUnitOfWork unitOfWork) : BaseManager<Job>(unitOfWork)
{


    public async Task<List<JobOpenRetrieveDTO>> GetListOpenJobAsync()
    {
        var projection = MapperHelper.CreateProjection<Job, JobOpenRetrieveDTO>();
        return await _repository.GetAllAsync(f => f.JobStatusId == JobStatusEnum.Open, projection);
    }



    public async Task<ApiResponse<PageResult<JobPaginationRetrieveDTO>>> GetListJobPagingAsync(JobPaginatedSearchRequest request)
    {

        PaginationParameter<Job> paginationParameter = MapperHelper.Map<PaginationParameter<Job>>(request);

        JobStatusEnum statusId = request.JobStatusId;

        if (statusId.IsNotDefault())
        {
            paginationParameter.Filters.Add(j => j.JobStatusId == statusId);
        }

        return await base.GetListPaginationAsync<JobPaginationRetrieveDTO>(paginationParameter);
    }



    public async Task<string> CreateNewJobAsync(JobCreateDTO jobForCreateDTO)
    {

        Job job = MapperHelper.Map<Job>(jobForCreateDTO);

        /*
        var levels = await MasterDataUtility.GetListLevelByIdListAsync(jobForCreateDTO.LevelIds);
        var skills = await MasterDataUtility.GetListSkillByIdListAsync(jobForCreateDTO.SkillIds);
        var benefits = await MasterDataUtility.GetListBenefitByIdListAsync(jobForCreateDTO.BenefitIds);

        job.AddSkills(skills);
        job.AddLevels(levels);
        job.AddBenefits(benefits);
        */
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


        //MapperHelper.Map(jobForUpdateDTO, jobFoundById);


        var levels = await _unitOfWork.LevelRepository.GetAllAsync<Level>(s => jobForUpdateDTO.LevelIds.Contains(s.Id), isTracking: true);
        var skills = await _unitOfWork.SkillRepository.GetAllAsync<Skill>(l => jobForUpdateDTO.SkillIds.Contains(l.Id), isTracking: true);
        var benefits = await _unitOfWork.BenefitRepository.GetAllAsync<Benefit>(b => jobForUpdateDTO.BenefitIds.Contains(b.Id), isTracking: true);


        jobFoundById.AddSkills(skills);
        jobFoundById.AddLevels(levels);
        jobFoundById.AddBenefits(benefits);


        _repository.Update(jobFoundById);
        bool updatedSuccessful = await _unitOfWork.SaveChangesAsync();

        ApplicationException.ThrowIfOperationFail(updatedSuccessful, "Fail to create new job");

        return "Update job successfully";
    }
}



