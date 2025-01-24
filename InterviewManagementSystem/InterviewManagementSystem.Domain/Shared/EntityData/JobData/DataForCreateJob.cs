namespace InterviewManagementSystem.Domain.Shared.EntityData.JobData
{
    public record DataForCreateJob : BaseJobData
    {
        public bool IsSaveAsDraft { get; set; }
    }

}
