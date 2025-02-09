namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;

/*
public sealed record CandidateForRetrieveDTO : UserForRetrieveDTO
{
    public DateTime? CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public string? UpdateBy { get; set; }
    public byte[]? Attachment { get; set; }
    public short YearsOfExperience { get; set; }
    public string? RecruiterName { get; set; }


    public PositionEnum PositionId { get; set; }
    public string? Position => PositionId.GetEnumName();


    public HighestLevelEnum HighestLevelId { get; set; }
    public string? HighestLevel => HighestLevelId.GetEnumName();


    public CandidateStatusEnum CandidateStatusId { get; set; }
    public string? CandidateStatus => CandidateStatusId.GetEnumName();



    public List<string> Offers { get; set; } = [];
    public List<string>? Skills { get; set; } = [];


    [JsonIgnore]
    public new DepartmentEnum DepartmentId { get; set; }

    [JsonIgnore]
    public new string? Department { get; set; }
}
*/



public record CandidateForRetrieveDTO : BaseCandidateDTO
{
    public Guid Id { get; set; }
}


public sealed record CandidateForDetailRetrieveDTO : CandidateForRetrieveDTO
{
    public byte[]? Attachment { get; set; }
    public List<string> Offers { get; set; } = [];
    public List<string>? Skills { get; set; } = [];
    public AuditInformation? AuditInformation { get; set; }
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