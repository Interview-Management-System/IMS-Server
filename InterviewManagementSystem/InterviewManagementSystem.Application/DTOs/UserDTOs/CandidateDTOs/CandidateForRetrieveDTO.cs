using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Enums.Extensions;
using Newtonsoft.Json;

namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;


public sealed record CandidateForRetrieveDTO : UserForRetrieveDTO
{
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
