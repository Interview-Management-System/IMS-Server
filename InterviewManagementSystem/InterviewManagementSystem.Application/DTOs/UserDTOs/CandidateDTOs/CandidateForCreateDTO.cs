using Microsoft.AspNetCore.Http;

namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs
{
    public record CandidateForCreateDTO : BaseUserDTO
    {
        public IFormFile? Attachment { get; set; }
        public int Status { get; set; }
        public Guid RecruiterId { get; set; }
        public PositionEnum PositionId { get; set; }
        public SkillsEnum[] SkillId { get; set; } = [];
        public byte YearsOfExperience { get; set; }
        public HighestLevelEnum HighestLevelId { get; set; }
    }
}
