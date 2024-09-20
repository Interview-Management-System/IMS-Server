namespace InterviewManagementSystem.Domain.Enums;

public enum BenefitEnum : byte
{
    Travel = 1,
    HybridWorking,
    HealthcareInsurance,
    DayLeave25,
    Lunch,
}





public static class BenefitEnumExtension
{
    private static readonly Dictionary<BenefitEnum, string> BenefitEnumMap = new()
    {
        {  BenefitEnum.Lunch, "Lunch"},
        {  BenefitEnum.DayLeave25, "25 Day Leave" },
        {  BenefitEnum.HealthcareInsurance, "Healthcare Insurance" },
        {  BenefitEnum.HybridWorking, "Hybrid Working" },
        {  BenefitEnum.Travel, "Travel" },

    };

    public static string GetBenefitName(this BenefitEnum status)
    {
        if (BenefitEnumMap.TryGetValue(status, out string? name))
            return name;

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }
}