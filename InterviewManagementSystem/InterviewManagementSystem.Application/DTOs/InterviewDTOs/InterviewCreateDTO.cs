namespace InterviewManagementSystem.Application.DTOs.InterviewDTOs
{
    public sealed record InterviewCreateDTO
    {
        public Guid? JobId { get; set; }
        public string? Note { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public string? MeetingId { get; set; }
        public Guid? CandidateId { get; set; }
        public Guid? RecruiterOwnerId { get; set; }
        public HourPeriod? HourPeriod { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public List<Guid> InterviewerIds { get; set; } = [];

    }
}
