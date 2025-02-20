namespace InterviewManagementSystem.Application.DTOs.InterviewDTOs
{
    public sealed record InterviewPaginatedSearchRequest : PaginatedSearchRequest
    {
        public Guid? InterviewerId { get; set; }
        public InterviewStatusEnum InterviewStatusId { get; set; } = InterviewStatusEnum.Default;
    }
}
