using Microsoft.AspNetCore.Http;

namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs
{
    public record CandidateForCreateDTO : BaseUserDTO
    {
        public IFormFile? Attachment { get; set; }
        public required ProfessionalInformation ProfessionalInformation { get; set; }
    }
}
