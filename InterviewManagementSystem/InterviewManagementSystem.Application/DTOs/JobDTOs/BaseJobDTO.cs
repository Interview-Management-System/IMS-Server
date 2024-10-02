namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public abstract record BaseJobDTO
    {
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
        public string? WorkingAddress { get; set; }
    }
}
