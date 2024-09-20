namespace InterviewManagementSystem.Domain.Enums;

public enum HighestLevelEnum : byte
{
    HighSchool = 1,
    BachelorDegree,
    MasterDegree,
    PhD
}



public static class HighestLevelEnumExtensions
{
    private static readonly Dictionary<HighestLevelEnum, string> HighestLevelEnumMap = new()
    {
        {  HighestLevelEnum.HighSchool, "High School" },
        {  HighestLevelEnum.BachelorDegree, "Bachelor Degree" },
        {  HighestLevelEnum.MasterDegree, "Master Degree" },
        {  HighestLevelEnum.PhD, "Doctor of Philosophy" },

    };

    public static string GetHighestLevelName(this HighestLevelEnum status)
    {
        if (HighestLevelEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }
}
