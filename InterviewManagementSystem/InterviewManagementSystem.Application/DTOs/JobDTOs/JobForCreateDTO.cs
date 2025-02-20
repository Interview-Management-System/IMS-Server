namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public record JobForCreateDTO : BaseJobDTO
    {
        public LevelEnum[] LevelIds { get; set; } = [];
        public BenefitEnum[] BenefitIds { get; set; } = [];
        public SkillsEnum[] SkillIds { get; set; } = [];
    }
}
