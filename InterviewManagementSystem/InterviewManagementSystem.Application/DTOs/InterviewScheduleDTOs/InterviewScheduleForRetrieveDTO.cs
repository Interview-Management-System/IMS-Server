namespace InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs
{
    public sealed record InterviewScheduleForRetrieveDTO : BaseInterviewSchedule
    {
        [Required]
        public Guid Id { get; set; }
    }
}
