namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs
{
    public sealed record CandidateForUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }
        public string? Note { get; set; } = null;
        public PersonalInformation? PersonalInformation { get; set; }
        public ProfessionalInformation? ProfessionalInformation { get; set; }
    }
}
