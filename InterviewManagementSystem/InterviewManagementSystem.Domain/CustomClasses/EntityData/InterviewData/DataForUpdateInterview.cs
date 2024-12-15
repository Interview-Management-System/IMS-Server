using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.CustomClasses.EntityData.InterviewData
{
    public sealed record DataForUpdateInterview : BaseInterviewData
    {
        public CandidateStatusEnum CandidateStatusId { get; set; }
    }
}
