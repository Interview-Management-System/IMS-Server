using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class Benefit
{
    public BenefitEnum Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}




public partial class Benefit
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