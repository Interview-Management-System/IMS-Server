using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Entities.MasterData;

public partial class Level
{
    public LevelEnum Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}



public partial class Level
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