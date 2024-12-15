namespace InterviewManagementSystem.Domain.CustomClasses.EntityData.JobData
{
    public record BaseJobData
    {
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
        public string? WorkingAddress { get; set; }
        public string? Description { get; set; }
        public List<Skill> Skills { get; set; } = [];
        public List<Level> Levels { get; set; } = [];
        public List<Benefit> Benefits { get; set; } = [];
    }
}
