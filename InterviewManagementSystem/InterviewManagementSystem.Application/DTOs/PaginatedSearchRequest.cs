namespace InterviewManagementSystem.Application.DTOs;

public record PaginatedSearchRequest
{
    public string? SearchText { get; set; }
    public PaginationRequest PaginationRequest { get; set; } = new();
}
