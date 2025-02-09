using InterviewManagementSystem.Domain.Shared.Paginations;

namespace InterviewManagementSystem.Application.DTOs;

public record PaginatedSearchRequest
{
    public bool IsLoadDeleted { get; set; } = false;
    public string? SearchText { get; set; } = string.Empty;
    public SortCriteria SortCriteria { get; set; } = new();
    public PaginationRequest PaginationRequest { get; set; } = new();
}
