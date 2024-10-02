using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Entities.MasterData;
using InterviewManagementSystem.Domain.Entities.Offers;

namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs
{
    public sealed record CandidateForRetrieveDTO : UserForRetrieveDTO
    {

        public short YearsOfExperience { get; set; }
        public string? RecruiterName { get; set; }
        public string? Position { get; set; }
        public string? HighestLevel { get; set; }
        public string? CandidateStatus { get; set; }


        public List<Offer> Offers { get; set; } = [];
        public List<Skill>? Skills { get; set; } = [];
    }
}
