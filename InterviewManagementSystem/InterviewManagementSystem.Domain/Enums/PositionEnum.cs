namespace InterviewManagementSystem.Domain.Enums;

public enum PositionEnum : short
{
    BackendDeveloper = 1,
    BusinessAnalyst,
    Tester,
    HR,
    ProjectManager,
    NotAvailable
}






public static class PositionEnumExtensions
{
    private static readonly Dictionary<PositionEnum, string> PositionEnumMap = new()
    {
        {  PositionEnum.BackendDeveloper, "Backend Developer" },
        {  PositionEnum.BusinessAnalyst, "Business Analyst" },
        {  PositionEnum.Tester, "Tester" },
        {  PositionEnum.HR, "Human Resource" },
        {  PositionEnum.ProjectManager, "Project Manager"},
        {  PositionEnum.NotAvailable, "Not Available" },

    };

    public static string GetPositionName(this PositionEnum status)
    {
        if (PositionEnumMap.TryGetValue(status, out string? name))
            return name;

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }


    public static string GetPositionNameById(this short status)
    {
        if (Enum.IsDefined(typeof(PositionEnum), status))
        {
            var position = (PositionEnum)status;
            return position.GetPositionName();
        }
        throw new ArgumentException($"Invalid Job status ID {status}");
    }
}