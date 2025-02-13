namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs
{
    public sealed record CandidatePaginatedSearchRequest : PaginatedSearchRequest
    {
        public CandidateStatusEnum StatusId { get; set; } = CandidateStatusEnum.Default;
    }
}
