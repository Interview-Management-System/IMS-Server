namespace InterviewManagementSystem.Domain.Enums.Helpers;

public static class CandidateEnumHelper
{

    public static List<CandidateStatusEnum> AllowedCandidateStatusForInterview =>
        [
            CandidateStatusEnum.Open,
            CandidateStatusEnum.WaitingForInterview,
            CandidateStatusEnum.InProgress,
            CandidateStatusEnum.PassedInterview,
            CandidateStatusEnum.WaitingForApproval
        ];
}
