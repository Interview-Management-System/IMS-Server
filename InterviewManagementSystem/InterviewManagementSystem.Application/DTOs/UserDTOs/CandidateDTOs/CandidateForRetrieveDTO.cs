namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;

public record CandidateForRetrieveDTO : BaseCandidateDTO
{
    public Guid Id { get; set; }
}


public sealed record CandidateForDetailRetrieveDTO : CandidateForRetrieveDTO
{
    public string? Gender { get; set; }
    public string? AttachmentLink { get; set; }
    public List<string> Offers { get; set; } = [];
    public List<string>? Skills { get; set; } = [];
    public AuditInformation AuditInformation { get; set; } = new();
    public ProfessionalInformation ProfessionalInformation { get; set; } = new();
    public string? Position => ProfessionalInformation.GetPositionName();
    public string? HighestLevel => ProfessionalInformation.GetHighestLevelName();
    public string? CandidateStatus => ProfessionalInformation.GetCandidateStatusName();
}



public sealed record CandidateForPaginationRetrieveDTO
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public bool IsDeleted { get; set; }
    public string? OwnerHr { get; set; }
    public string? Username { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CurrentPosition { get; set; }
    public string? CandidateStatus { get; set; }
    public UserStatus? UserStatus { get; set; }
}