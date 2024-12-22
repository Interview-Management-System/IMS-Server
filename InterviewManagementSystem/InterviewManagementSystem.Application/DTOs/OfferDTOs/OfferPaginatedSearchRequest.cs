namespace InterviewManagementSystem.Application.DTOs.OfferDTOs;

public sealed record OfferPaginatedSearchRequest : PaginatedSearchRequest
{
    public DepartmentEnum? DepartmentId { get; set; } = default;
    public OfferStatusEnum? OfferStatusId { get; set; } = default;
}
