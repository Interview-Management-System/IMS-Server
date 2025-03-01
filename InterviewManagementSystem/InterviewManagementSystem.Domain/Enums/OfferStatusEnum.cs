using System.ComponentModel;

namespace InterviewManagementSystem.Domain.Enums;

public enum OfferStatusEnum
{
    Default,

    [Description("Waiting For Approval")]
    WaitingForApproval = 1,

    [Description("Approved")]
    Approved,

    [Description("Rejected")]
    Rejected,

    [Description("Waiting For Response")]
    WaitingForResponse,

    [Description("Cancelled")]
    Cancelled,

    [Description("Accepted")]
    Accepted,

    [Description("Declined")]
    Declined
}



