using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class Skill
{
    public SkillsEnum Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}




public partial class Skill
{


    public void AddJob(Job job)
    {
        Jobs.Add(job);
    }



    public void RemoveJob(Job job)
    {
        Jobs.Remove(job);
    }
}