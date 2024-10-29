namespace InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs
{
    public sealed record InterviewScheduleForRetrieveDTO : BaseInterviewSchedule
    {
        [Required]
        public Guid Id { get; set; }
        public string? StartHour { get; set; }
        public string? EndHour { get; set; }
    }
}
