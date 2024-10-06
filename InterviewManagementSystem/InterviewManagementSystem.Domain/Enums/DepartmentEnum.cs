namespace InterviewManagementSystem.Domain.Enums;

public enum DepartmentEnum : short
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
        {  DepartmentEnum.IT, "IT" },
        {  DepartmentEnum.HR, "Human Resource" },
        {  DepartmentEnum.Finance, "Finance" },
        {  DepartmentEnum.Communication, "Communication" },
        {  DepartmentEnum.Marketing, "Marketing"},
        {  DepartmentEnum.Accounting, "Accounting" },

    };

    public static string GetDepartmentName(this DepartmentEnum status)
    {
        if (DepartmentEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }


    public static string GetDepartmentNameById(this short id)
    {
        if (Enum.IsDefined(typeof(DepartmentEnum), id))
        {
            var department = (DepartmentEnum)id;
            return department.GetDepartmentName();
        }
        throw new ArgumentException($"Invalid department ID {id}");
    }
}