namespace InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs
{
    public record InterviewScheduleForCreateDTO
    {
        public string? Title { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime ScheduleTime { get; set; }
        public string? StartHour { get; set; }
        public string? EndHour { get; set; }
        public string? Note { get; set; }
        public string? Location { get; set; }
        public Guid JobId { get; set; }
        public Guid[] InterviewerList { get; set; } = [];
        public string? MeetingUrl { get; set; }
        public Guid RecruiterOwnerId { get; set; }
    }
}
