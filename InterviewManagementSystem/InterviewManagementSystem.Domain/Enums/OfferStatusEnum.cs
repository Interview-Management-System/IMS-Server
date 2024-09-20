namespace InterviewManagementSystem.Domain.Enums;

public enum OfferStatusEnum : byte
{
    WaitingForApproval = 1,
    Approved,
    Rejected,
    WaitingForResponse,
    Cancelled,
    Accepted,
    Declined
}




public static class OfferStatusEnumExtension
{
    private static readonly Dictionary<OfferStatusEnum, string> OfferStatusEnumMap = new()
    {
        {  OfferStatusEnum.WaitingForApproval, "Waiting for approval" },
        {  OfferStatusEnum.Approved, nameof(OfferStatusEnum.Approved) },
        {  OfferStatusEnum.Rejected, nameof(OfferStatusEnum.Rejected)  },
        {  OfferStatusEnum.WaitingForResponse, "Waiting for response" },
        {  OfferStatusEnum.Cancelled, nameof(OfferStatusEnum.Cancelled) },
        {  OfferStatusEnum.Accepted, nameof(OfferStatusEnum.Accepted)  },
        {  OfferStatusEnum.Declined, nameof(OfferStatusEnum.Declined)  },

    };

    public static string GetOfferStatusName(this OfferStatusEnum status)
    {
        if (OfferStatusEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }
}