using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Shared.EntityData.InterviewData
{
    public sealed record DataForUpdateInterview : BaseInterviewData
    {
        public CandidateStatusEnum CandidateStatusId { get; set; }
    }
}
