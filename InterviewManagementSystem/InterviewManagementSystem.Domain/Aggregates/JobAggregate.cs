using InterviewManagementSystem.Domain.CustomClasses;
using InterviewManagementSystem.Domain.Interfaces;

namespace InterviewManagementSystem.Domain.Aggregates;
public sealed class JobAggregate
{

    public Job Job => _job;  // Aggregate Root
    private readonly Job _job;  // Aggregate Root
    private readonly IUnitOfWork _unitOfWork;





    public JobAggregate(Job job, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _job = job ?? throw new ArgumentNullException(nameof(job));
    }





    public async Task<Job> CreateJobAsync(JobMasterData jobMasterData)
    {
        var datePeriod = DatePeriod.Create(jobMasterData.StartDate, jobMasterData.EndDate);
        var salaryRange = SalaryRange.CreateSalaryRange(jobMasterData.From, jobMasterData.To);

        _job.SaveDraft();
        _job.GenerateId();
        _job.SetDatePeriod(datePeriod);
        _job.SetSalaryRange(salaryRange);

        await SetSkillsAsync(jobMasterData.SkillIdList);
        await SetLevelsAsync(jobMasterData.LevelIdList);
        await SetBenefitsAsync(jobMasterData.BenefitIdList);

        return _job;
    }




    public async Task UpdateJobAsync(JobMasterData jobMasterData)
    {
        var datePeriod = DatePeriod.Create(jobMasterData.StartDate, jobMasterData.EndDate);
        var salaryRange = SalaryRange.CreateSalaryRange(jobMasterData.From, jobMasterData.To);

        _job.SetDatePeriod(datePeriod);
        _job.SetSalaryRange(salaryRange);


        await SetSkillsAsync(jobMasterData.SkillIdList);
        await SetLevelsAsync(jobMasterData.LevelIdList);
        await SetBenefitsAsync(jobMasterData.BenefitIdList);
    }

    /*
    private async Task SetLevelsAsync(short[] levelIdList)
    {

        _job.ClearLevels();
        var tasks = levelIdList.Select(async levelId =>
        {
            // Ensure a separate DbContext is used for each query if necessary
            return await _unitOfWork.LevelRepository.GetByIdAsync(levelId, true);
        }).ToList();

        // 2. Await all tasks to complete concurrently
        var levels = await Task.WhenAll(tasks);




        foreach (var level in levels)
        {
            //var level = await levelRepository.GetByIdAsync(levelId, true);

            if (level != null && !_job.HasLevel(level.Id))
            {
                level.AssignJob(_job);
                _job.AddLevel(level);
            }
        }


        
        if (levelIdList.Length != 0)
        {
            var levelRepository = _unitOfWork.GetBaseRepository<Level>();


        }
}
    */


    private async Task SetSkillsAsync(short[] skillIdList)
    {
        _job.ClearSkills();

        if (skillIdList.Length != 0)
        {

            var skillRepository = _unitOfWork.SkillRepository;

            foreach (var skillId in skillIdList)
            {
                var skill = await skillRepository.GetByIdAsync(skillId, true);
                if (skill != null && !_job.HasSkill(skill.Id))
                {
                    skill.AssignJob(_job);
                    _job.AddSkill(skill);
                }
            }

        }



    }



    private async Task SetLevelsAsync(short[] levelIdList)
    {

        _job.ClearLevels();

        if (levelIdList.Length != 0)
        {
            var levelRepository = _unitOfWork.GetBaseRepository<Level>();

            foreach (var levelId in levelIdList)
            {
                var level = await levelRepository.GetByIdAsync(levelId, true);

                if (level != null && !_job.HasLevel(level.Id))
                {
                    level.AssignJob(_job);
                    _job.AddLevel(level);
                }
            }

        }
    }



    private async Task SetBenefitsAsync(short[] benefitIdList)
    {

        _job.ClearBenefits();


        if (benefitIdList.Length != 0)
        {

            var benefitRepository = _unitOfWork.GetBaseRepository<Benefit>();

            foreach (var benefitId in benefitIdList)
            {

                var benefit = await benefitRepository.GetByIdAsync(benefitId, true);

                if (benefit != null && !_job.HasBenefit(benefit.Id))
                {
                    benefit.AssignJob(_job);
                    _job.AddBenefit(benefit);
                }
            }
        }
    }
}