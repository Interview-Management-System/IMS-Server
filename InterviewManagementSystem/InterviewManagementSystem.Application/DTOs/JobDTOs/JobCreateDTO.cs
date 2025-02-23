namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public record JobCreateDTO : BaseJobDTO
    {
        public LevelEnum[] LevelIds { get; set; } = [];
        public BenefitEnum[] BenefitIds { get; set; } = [];
        public SkillsEnum[] SkillIds { get; set; } = [];
    }
}
