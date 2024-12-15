using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Entities.Offers;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs
{

    [DataContract]
    public sealed record CandidateForRetrieveDTO : UserForRetrieveDTO
    {

        public short YearsOfExperience { get; set; }
        public string? RecruiterName { get; set; }


        public PositionEnum PositionId { get; set; }
        public string? Position { get; set; }


        public HighestLevelEnum HighestLevelId { get; set; }
        public string? HighestLevel { get; set; }


        public CandidateStatusEnum CandidateStatusId { get; set; }
        public string? CandidateStatus { get; set; }


        public byte[]? Attachment { get; set; }

        public List<Offer> Offers { get; set; } = [];
        public List<Skill>? Skills { get; set; } = [];


        [JsonIgnore]
        [DataMember]
        public new DepartmentEnum DepartmentId { get; set; }

        [JsonIgnore]
        [DataMember]
        public new string? Department { get; set; }
    }
}
