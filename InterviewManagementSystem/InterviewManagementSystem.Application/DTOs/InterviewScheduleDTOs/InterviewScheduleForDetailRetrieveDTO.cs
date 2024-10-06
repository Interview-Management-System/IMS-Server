namespace InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs
{
    public sealed record InterviewScheduleForDetailRetrieveDTO : BaseInterviewSchedule
    {
        [Required]
        public Guid Id { get; set; }
        public string? Note { get; set; }
        public string? MeetingURL { get; set; }
        public string? Location { get; set; }
        public string? RecruiterOwner { get; set; }
    }
}
