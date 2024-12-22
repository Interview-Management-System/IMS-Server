namespace InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs
{
    public sealed record InterviewSchedulePaginatedSearchRequest : PaginatedSearchRequest
    {
        public Guid? InterviewerId { get; set; }
        public InterviewStatusEnum? InterviewStatusId { get; set; }
    }
}
