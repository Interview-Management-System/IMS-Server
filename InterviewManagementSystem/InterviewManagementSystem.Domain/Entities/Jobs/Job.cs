using InterviewManagementSystem.Domain.Enums;
using InterviewManagementSystem.Domain.Shared.Utilities;

namespace InterviewManagementSystem.Domain.Entities.Jobs;


#region Poperties
public partial class Job : BaseEntity
{
    public string? Title { get; set; }

    public string? WorkingAddress { get; set; }

    public string? Description { get; set; }

    public SalaryRange? SalaryRange { get; set; }

    public DatePeriod? DatePeriod { get; set; }

    public JobStatusEnum? JobStatusId { get; set; }

    public virtual AppUser? CreatedByNavigation { get; set; }

    public virtual JobStatus? JobStatus { get; set; }

    public virtual AppUser? UpdatedByNavigation { get; set; }

    public virtual ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();

    public virtual ICollection<Level> Levels { get; set; } = new List<Level>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();



    public Job()
    {
        JobStatusId = JobStatusEnum.Draft;
    }

}
#endregion



#region Support methods
public partial class Job
{

    public void AddCandidate(Candidate candidate)
    {
        Candidates.Add(candidate);
    }



    public void AddSkills(List<Skill> skills)
    {
        Skills.Clear();

        if (skills.Count != 0)
        {
            var skillsToRemove = EntityComparer.GetNonMatchingEntities(Skills, skills);
            foreach (var skill in skillsToRemove)
            {
                Skills.Remove(skill);
                skill.RemoveJob(this);
            }


            var skillsToAdd = EntityComparer.GetNonMatchingEntities(skills, Skills);
            foreach (var skill in skillsToAdd)
            {
                Skills.Add(skill);
                skill.AddJob(this);
            }
        }
    }




    public void AddLevels(List<Level> levels)
    {
        Levels.Clear();


        var levelsToRemove = EntityComparer.GetNonMatchingEntities(Levels, levels);
        foreach (var level in levelsToRemove)
        {
            Levels.Remove(level);
            level.RemoveJob(this);
        }


        var levelsToAdd = EntityComparer.GetNonMatchingEntities(levels, Levels);
        foreach (var newLevel in levelsToAdd)
        {
            Levels.Add(newLevel);
            newLevel.AddJob(this);
        }
    }




    public void AddBenefits(List<Benefit> benefits)
    {
        Benefits.Clear();


        var benefitsToRemove = EntityComparer.GetNonMatchingEntities(Benefits, benefits);
        foreach (var benefit in benefitsToRemove)
        {
            Benefits.Remove(benefit);
            benefit.RemoveJob(this);
        }



        var benefitsToAdd = EntityComparer.GetNonMatchingEntities(benefits, Benefits);
        foreach (var benefit in benefitsToAdd)
        {
            Benefits.Add(benefit);
            benefit.AddJob(this);
        }

    }





    public void SaveAsDraft()
    {
        SetJobStatus(JobStatusEnum.Draft);
    }


    public void OpenJob()
    {
        SetJobStatus(JobStatusEnum.Open);
    }



    public void CloseJob()
    {
        SetJobStatus(JobStatusEnum.Closed);
    }


    private void SetJobStatus(JobStatusEnum jobStatusEnum)
    {
        JobStatusId = jobStatusEnum;
    }


    public void DeleteJob()
    {
        IsDeleted = true;
    }



    public void AddInterviewSchedule(InterviewSchedule interviewSchedule)
    {
        InterviewSchedules.Add(interviewSchedule);
    }





    public void RemoveLevel(Level level)
    {
        Levels.Remove(level);
    }
}
#endregion