namespace InterviewManagementSystem.Domain.Enums;

public enum SkillsEnum : byte
{
    Java = 1,
    NodeJs,
    DotNet,
    CPlus,
    BusinessAnalysis,
    Communication,
}





public static class SkillsEnumExtensions
{
    private static readonly Dictionary<SkillsEnum, string> SkillsEnumMap = new()
    {
        {  SkillsEnum.Java, "Java" },
        {  SkillsEnum.NodeJs, "NodeJs" },
        {  SkillsEnum.DotNet, ".NET" },
        {  SkillsEnum.CPlus, "C++" },
        {  SkillsEnum.BusinessAnalysis, "Business Analysis"},
        {  SkillsEnum.Communication, "Communication" },

    };

    public static string GetSkillName(this SkillsEnum status)
    {
        if (SkillsEnumMap.TryGetValue(status, out string? name))
            return name;

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }
}