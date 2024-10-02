namespace InterviewManagementSystem.Domain.Enums;

public enum HighestLevelEnum : short
{
    HighSchool = 1,
    BachelorDegree,
    MasterDegree,
    PhD
}



public static class HighestLevelEnumExtension
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


    public static string GetHighestLevelNameById(this short id)
    {
        if (Enum.IsDefined(typeof(HighestLevelEnum), id))
        {
            var highestLevel = (HighestLevelEnum)id;
            return highestLevel.GetHighestLevelName();
        }
        throw new ArgumentException($"Invalid department ID {id}");
    }
}
