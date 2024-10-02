namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs
{
    public sealed record CandidateForUpdateDTO : CandidateForCreateDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
