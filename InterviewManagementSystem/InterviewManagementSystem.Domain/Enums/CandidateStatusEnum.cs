namespace InterviewManagementSystem.Domain.Enums;

public enum CandidateStatusEnum : short
{
    Default,
    Open,
    Banned,
    WaitingForInterview,
    InProgress,
    Cancelled,
    FailedInterview,
    PassedInterview,
    WaitingForApproval,
    RejectedOffer,
    ApprovedOffer,
    CancelledOffer,
    WaitingForResponse,
    AcceptedOffer,
    DeclinedOffer
}
