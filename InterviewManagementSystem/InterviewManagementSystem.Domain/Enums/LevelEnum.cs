namespace InterviewManagementSystem.Domain.Enums;

public enum LevelEnum : short
{
    Fresher = 1,
    Junior,
    Senior,
    Leader,
    Manager,
    ViceHead,
}





public static class LevelEnumExtension
{
    private static readonly Dictionary<LevelEnum, string> LevelEnumMap = new()
    {
        {  LevelEnum.Fresher, "Fresher" },
        {  LevelEnum.Junior, "Junior" },
        {  LevelEnum.Senior, "Senior" },
        {  LevelEnum.Leader, "Leader" },
        {  LevelEnum.Manager, "Manager"},
        {  LevelEnum.ViceHead, "Vice Head" },

    };

    public static string GetLevelName(this LevelEnum status)
    {
        if (LevelEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }


    public static string GetLevelNameById(this short levelId)
    {
        if (Enum.IsDefined(typeof(LevelEnum), levelId))
        {
            var level = (LevelEnum)levelId;
            return level.GetLevelName();
        }
        throw new ArgumentException($"Invalid Job status ID {levelId}");
    }
}