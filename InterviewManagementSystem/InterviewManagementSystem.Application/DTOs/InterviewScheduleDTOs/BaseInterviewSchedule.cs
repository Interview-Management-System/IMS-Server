namespace InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs
{
    public abstract record BaseInterviewSchedule
    {
        public string? Title { get; set; }
        public string? CandidateName { get; set; }
        public string[]? Interviewers { get; set; }
        public string? Schedule { get; set; } = null;
        public string? Result { get; set; }
        public string? Status { get; set; }
        public string? Job { get; set; }
    }
}
