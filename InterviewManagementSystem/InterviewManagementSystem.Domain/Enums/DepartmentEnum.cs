namespace InterviewManagementSystem.Domain.Enums;

public enum DepartmentEnum : byte
{
    IT = 1,
    HR,
    Finance,
    Communication,
    Marketing,
    Accounting,
}





public static class DepartmentEnumExtensions
{
    private static readonly Dictionary<DepartmentEnum, string> DepartmentEnumMap = new()
    {
        {  DepartmentEnum.IT, "Backend Developer" },
        {  DepartmentEnum.HR, "Business Analyst" },
        {  DepartmentEnum.Finance, "Tester" },
        {  DepartmentEnum.Communication, "Human Resource" },
        {  DepartmentEnum.Marketing, "Project Manager"},
        {  DepartmentEnum.Accounting, "Not Available" },

    };

    public static string GetDepartmentName(this DepartmentEnum status)
    {
        if (DepartmentEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }
}