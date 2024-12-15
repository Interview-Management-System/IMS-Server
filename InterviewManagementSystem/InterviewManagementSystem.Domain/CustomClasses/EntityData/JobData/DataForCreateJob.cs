namespace InterviewManagementSystem.Domain.CustomClasses.EntityData.JobData
{
    public record DataForCreateJob : BaseJobData
    {
        public bool IsSaveAsDraft { get; set; }
    }

}
