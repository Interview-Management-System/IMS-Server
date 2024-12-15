namespace InterviewManagementSystem.Domain.CustomClasses.EntityData.InterviewData
{
    public record BaseInterviewData
    {
        public string? Title { get; set; }
        public Guid? CandidateId { get; set; }
        public DateTime ScheduleTime { get; set; }
        public string? Note { get; set; }
        public string? Location { get; set; }
        public Guid? JobId { get; set; }
        public string? MeetingUrl { get; set; }
        public Guid? RecruiterOwnerId { get; set; }
        public string EndHourString { get; set; } = default!;
        public string StartHourString { get; set; } = default!;
        public List<AppUser>? Interviews { get; set; } = [];
    }
}
