namespace InterviewManagementSystem.Domain.Enums;

public enum CandidateStatusEnum : short
{
    Open = 1,
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
