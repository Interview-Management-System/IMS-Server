namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public record JobPaginatedSearchRequest : PaginatedSearchRequest
    {
        public JobStatusEnum? JobStatusId { get; set; }
    }
}
