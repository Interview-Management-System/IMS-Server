namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public sealed record JobOpenForRetrieveDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
    }
}
