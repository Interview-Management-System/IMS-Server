using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.Jobs;

public partial class Job : BaseEntity
{
    public string? Title { get; set; }

    public string? WorkingAddress { get; set; }

    public string? Description { get; set; }

    public SalaryRange? SalaryRange { get; set; }

    public DatePeriod? DatePeriod { get; set; }

    public Guid? AppUserId { get; set; }

    public short? JobStatusId { get; set; }

    public virtual AppUser? AppUser { get; set; }

    public virtual AppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();

    public virtual JobStatus? JobStatus { get; set; }

    public virtual AppUser? UpdatedByNavigation { get; set; }

    public virtual ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();

    public virtual ICollection<Level> Levels { get; set; } = new List<Level>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();



    public Job()
    {
        SaveDraft();
        GenerateId();
    }
}





public partial class Job
{
    public void GenerateId()
    {
        Id = Guid.NewGuid();
    }


    public void SetDatePeriod(DatePeriod datePeriod)
    {
        DatePeriod = datePeriod;
    }


    public void SetSalaryRange(SalaryRange salaryRange)
    {
        SalaryRange = salaryRange;
    }


    public void ClearSkills()
    {
        Skills.Clear();
    }


    public void ClearLevels()
    {
        Levels.Clear();
    }


    public void ClearBenefits()
    {
        Benefits.Clear();
    }



    public void AddSkills(List<Skill> skills)
    {
        ClearSkills();

        if (skills.Count > 0)
        {
            Skills = skills;

            foreach (var skill in skills)
            {
                skill.AssignJob(this);
            }
        }
    }



    public void AddLevels(List<Level> levels)
    {
        ClearLevels();

        if (levels.Count > 0)
        {
            Levels = levels;

            foreach (var level in levels)
            {
                level.AssignJob(this);
            }
        }
    }


    public void AddBenefits(List<Benefit> benefits)
    {
        ClearBenefits();

        if (benefits.Count > 0)
        {
            Benefits = benefits;

            foreach (var benefit in benefits)
            {
                benefit.AssignJob(this);
            }
        }
    }


    public bool HasLevel(short levelId)
    {
        return Levels.Any(l => l.Id == levelId);
    }


    public bool HasSkill(short skillId)
    {
        return Skills.Any(l => l.Id == skillId);
    }


    public bool HasBenefit(short benefitId)
    {
        return Benefits.Any(l => l.Id == benefitId);
    }


    public void SaveDraft()
    {
        JobStatusId = (short)JobStatusEnum.Draft;
    }


    public void OpenJob()
    {
        JobStatusId = (short)JobStatusEnum.Open;
    }



    public void CloseJob()
    {
        JobStatusId = (short)JobStatusEnum.Closed;
    }


    public void DeleteJob()
    {
        IsDeleted = true;
    }
}