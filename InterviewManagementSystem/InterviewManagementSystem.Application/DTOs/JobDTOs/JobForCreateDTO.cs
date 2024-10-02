namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public record JobForCreateDTO : BaseJobDTO
    {
        public SkillsEnum[] RequiredSkillId { get; set; } = [];
        public BenefitEnum[] BenefitId { get; set; } = [];
        public LevelEnum[] LevelId { get; set; } = [];
        public string? Description { get; set; }
    }
}
