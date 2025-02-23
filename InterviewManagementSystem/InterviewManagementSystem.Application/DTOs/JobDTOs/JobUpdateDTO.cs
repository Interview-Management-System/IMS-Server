namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public sealed record JobUpdateDTO : JobCreateDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
