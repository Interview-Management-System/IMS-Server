namespace InterviewManagementSystem.Domain.Enums;

public enum RoleEnum
{
    Admin,
    Manager,
    Candidate,
    Recruiter,
    Interviewer,
}






public static class RoleEnumExtension
{
    private static readonly Dictionary<RoleEnum, string> RoleIdEnumMap = new()
    {
        {  RoleEnum.Admin, "5900eab6-e2e1-4160-83ee-b985ac709f46" },
        {  RoleEnum.Candidate, "83926601-feef-4371-8fbe-40a68fbe8ef6" },
        {  RoleEnum.Manager, "60de2025-9b80-4ce2-920c-4628860ccfce" },
        {  RoleEnum.Recruiter, "b13ce32b-05a7-480b-817f-099106c463a7" },
        {  RoleEnum.Interviewer, "9360eb9d-3aa2-4448-ba57-920cb69043af" },
    };


    private static readonly Dictionary<RoleEnum, string> RoleNameEnumMap = new()
    {
        {  RoleEnum.Admin, "Admin" },
        {  RoleEnum.Candidate, "Candidate" },
        {  RoleEnum.Manager, "Manager" },
        {  RoleEnum.Recruiter, "Recruiter" },
        {  RoleEnum.Interviewer, "Interviewer" },
    };


    public static string GetId(this RoleEnum status)
    {
        if (RoleIdEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }

    public static string GetName(this RoleEnum status)
    {
        if (RoleNameEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }
}
