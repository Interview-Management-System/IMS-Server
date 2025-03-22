namespace InterviewManagementSystem.Application.DTOs.InterviewDTOs;

public record InterviewRetrieveDTO : BaseInterviewDTO
{
    public Guid Id { get; set; }
    public string? JobTitle { get; set; }
    public string? CandidateName { get; set; }
    public string[] Interviewers { get; set; } = [];
}



public sealed record InterviewPaginationRetrieveDTO : InterviewRetrieveDTO { }


public sealed record InterviewDetailRetrieveDTO : InterviewRetrieveDTO
{
    public string? Note { get; set; }
    public string? Location { get; set; }
    public string? MeetingId { get; set; }
}