using InterviewManagementSystem.Domain.CustomClasses;
using InterviewManagementSystem.Domain.Interfaces;

namespace InterviewManagementSystem.Domain.Aggregates;


public sealed class JobAggregate
{

    private readonly Job _job; // Root
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
        //_job.SetLastModifiedDateIsNow();

        await Task.WhenAll
             (
                 SetSkillsAsync(jobMasterData.SkillIdList),
                 SetLevelsAsync(jobMasterData.LevelIdList),
                 SetBenefitsAsync(jobMasterData.BenefitIdList)
             );

        return _job;
    }




    public async Task UpdateJobAsync(JobMasterData jobMasterData)
    {

        var datePeriod = DatePeriod.Create(jobMasterData.StartDate, jobMasterData.EndDate);
        var salaryRange = SalaryRange.CreateSalaryRange(jobMasterData.From, jobMasterData.To);

        _job.SetDatePeriod(datePeriod);
        _job.SetSalaryRange(salaryRange);


        await Task.WhenAll
            (
                SetSkillsAsync(jobMasterData.SkillIdList),
                SetLevelsAsync(jobMasterData.LevelIdList),
                SetBenefitsAsync(jobMasterData.BenefitIdList)
            );
    }



    #region Private methods
    private async Task SetSkillsAsync(short[] skillIdList)
    {
        var skillList = await _unitOfWork.SkillRepository.GetAllAsync(s => skillIdList.Contains(s.Id), true);
        _job.AddSkills(skillList);
    }



    private async Task SetLevelsAsync(short[] levelIdList)
    {
        var levelList = await _unitOfWork.LevelRepository.GetAllAsync(l => levelIdList.Contains(l.Id), true);
        _job.AddLevels(levelList);
    }


    private async Task SetBenefitsAsync(short[] benefitIdList)
    {
        var benefitList = await _unitOfWork.BenefitRepository.GetAllAsync(s => benefitIdList.Contains(s.Id), true);
        _job.AddBenefits(benefitList);
    }
    #endregion

}