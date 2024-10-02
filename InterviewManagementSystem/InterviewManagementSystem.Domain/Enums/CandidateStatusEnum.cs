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




public static class CandidateStatusEnumExtension
{
    private static readonly Dictionary<CandidateStatusEnum, string> CandidateStatusEnumMap = new()
    {
        {  CandidateStatusEnum.Open, "Open" },
        {  CandidateStatusEnum.Banned, "Banned"},
        {  CandidateStatusEnum.WaitingForInterview, "Waiting For Interview" },
        {  CandidateStatusEnum.InProgress, "In Progress" },
        {  CandidateStatusEnum.Cancelled, "Cancelled"  },
        {  CandidateStatusEnum.FailedInterview, "Failed Interview"  },
        {  CandidateStatusEnum.PassedInterview, "Passed Interview" },
        {  CandidateStatusEnum.WaitingForApproval, "Waiting For Approval" },
        {  CandidateStatusEnum.RejectedOffer, "Rejected Offer"},
        {  CandidateStatusEnum.ApprovedOffer, "Approved Offer" },
        {  CandidateStatusEnum.CancelledOffer, "Cancelled Offer" },
        {  CandidateStatusEnum.WaitingForResponse, "Waiting For Response"  },
        {  CandidateStatusEnum.AcceptedOffer, "Accepted Offer"  },
        {  CandidateStatusEnum.DeclinedOffer, "Declined Offer"},
    };

    public static string GetCandidateStatusName(this CandidateStatusEnum status)
    {
        if (CandidateStatusEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }



    public static string GetCandidateStatusNameById(this short status)
    {
        if (Enum.IsDefined(typeof(CandidateStatusEnum), status))
        {
            var candidateStatus = (CandidateStatusEnum)status;
            return candidateStatus.GetCandidateStatusName();
        }
        throw new ArgumentException($"Invalid department ID {status}");
    }
}