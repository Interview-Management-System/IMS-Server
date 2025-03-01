using System.ComponentModel;

namespace InterviewManagementSystem.Domain.Enums;

public enum CandidateStatusEnum : short
{
    Default,

    [Description("Open")]
    Open,

    [Description("Banned")]
    Banned,

    [Description("Waiting For Interview")]
    WaitingForInterview,

    [Description("InProgress")]
    InProgress,

    [Description("Cancelled")]
    Cancelled,

    [Description("Failed Interview")]
    FailedInterview,

    [Description("Passed Interview")]
    PassedInterview,

    [Description("Waiting For Approval")]
    WaitingForApproval,

    [Description("Rejected Offer")]
    RejectedOffer,

    [Description("Approved Offer")]
    ApprovedOffer,

    [Description("Cancelled Offer")]
    CancelledOffer,

    [Description("Waiting For Response")]
    WaitingForResponse,

    [Description("Accepted Offer")]
    AcceptedOffer,

    [Description("Declined Offer")]
    DeclinedOffer
}
