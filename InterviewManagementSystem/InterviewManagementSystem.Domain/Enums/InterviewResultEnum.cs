namespace InterviewManagementSystem.Domain.Enums;

public enum InterviewResultEnum : short
{
    Open = 1,
    Pass,
    Failed,
    NA
}





public static class InterviewResultEnumExtension
{
    private static readonly Dictionary<InterviewResultEnum, string> InterviewResultEnumMap = new()
    {
        {  InterviewResultEnum.Open, "Open"},
        {  InterviewResultEnum.Pass, "Pass"},
        {  InterviewResultEnum.Failed, "Failed"},
        {  InterviewResultEnum.NA, "N/A"},
    };



    public static string GetInterviewResultName(this InterviewResultEnum status)
    {
        if (InterviewResultEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }




    public static string GetInterviewResultNameById(this short status)
    {
        if (Enum.IsDefined(typeof(InterviewResultEnum), status))
        {
            var interviewResult = (InterviewResultEnum)status;
            return interviewResult.GetInterviewResultName();
        }
        throw new ArgumentException($"Invalid Job status ID {status}");
    }
}