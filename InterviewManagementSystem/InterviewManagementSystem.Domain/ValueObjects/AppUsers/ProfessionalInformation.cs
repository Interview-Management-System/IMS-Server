using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.ValueObjects.AppUsers
{
    public sealed record ProfessionalInformation
    {
        public Guid RecruiterId { get; set; }
        public PositionEnum PositionId { get; set; }
        public SkillsEnum[] SkillId { get; set; } = [];
        public byte YearsOfExperience { get; set; }
        public DepartmentEnum DepartmentId { get; set; }
        public HighestLevelEnum HighestLevelId { get; set; }
    }
}
