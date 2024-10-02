namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public sealed record JobForUpdateDTO : JobForCreateDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
