namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public abstract record BaseJobDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? WorkingAddress { get; set; }
        public DatePeriod? DatePeriod { get; set; } = new();
        public SalaryRange? SalaryRange { get; set; } = new();
    }



    public sealed record JobStatus
    {
        public JobStatusEnum? JobStatusId { get; set; }
        public string? Status => JobStatusId.GetValueOrDefault().GetDescription();
    }


    public sealed record SalaryRange
    {
        public decimal From { get; set; } = 0;
        public decimal To { get; set; } = 0;
    }


    public sealed record DatePeriod
    {
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
