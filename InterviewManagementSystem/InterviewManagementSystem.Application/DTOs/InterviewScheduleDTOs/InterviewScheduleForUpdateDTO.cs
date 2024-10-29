namespace InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs
{
    public sealed record InterviewScheduleForUpdateDTO : InterviewScheduleForCreateDTO
    {
        [Required]
        public Guid Id { get; set; }

        public CandidateStatusEnum CandidateStatusId { get; set; }
    }
}
