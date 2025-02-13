namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;

public abstract record BaseCandidateDTO
{
    public string? Note { get; set; }
    public PersonalInformation PersonalInformation { get; set; } = new();
}


public sealed record ProfessionalInformation
{
    public Guid RecruiterId { get; set; }
    public short YearsOfExperience { get; set; }
    public PositionEnum PositionId { get; set; }
    public HighestLevelEnum HighestLevelId { get; set; }
    public CandidateStatusEnum CandidateStatusId { get; set; }


    public string GetPositionName()
    {
        return PositionId.GetEnumName();
    }


    public string GetHighestLevelName()
    {
        return HighestLevelId.GetEnumName();
    }

    public string GetCandidateStatusName()
    {
        return CandidateStatusId.GetEnumName();
    }
}
