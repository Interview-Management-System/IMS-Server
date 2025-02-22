using Microsoft.AspNetCore.Http;

namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs
{
    public record CandidateForCreateDTO : BaseUserDTO
    {
        public IFormFile? Avatar { get; set; }
        public IFormFile? Attachment { get; set; }
        public SkillsEnum[] SkillList { get; set; } = [];
        public required ProfessionalInformation ProfessionalInformation { get; set; }
    }
}
