namespace InterviewManagementSystem.Domain.Enums;



public enum InterviewStatusEnum : short
{
    New = 1,
    Invited,
    Interviewed,
    Cancelled
}




public static class InterviewStatusEnumExtension
{
    private static readonly Dictionary<InterviewStatusEnum, string> InterviewStatusEnumMap = new()
    {
        {  InterviewStatusEnum.New, "New"},
        {  InterviewStatusEnum.Invited, "Invited"},
        {  InterviewStatusEnum.Interviewed, "Interviewed"},
        {  InterviewStatusEnum.Cancelled, "Cancelled"},
    };



    public static string GetInterviewStatusName(this InterviewStatusEnum status)
    {
        if (InterviewStatusEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }




    public static string GetInterviewStatusNameById(this short status)
    {
        if (Enum.IsDefined(typeof(InterviewStatusEnum), status))
        {
            var interviewStatus = (InterviewStatusEnum)status;
            return interviewStatus.GetInterviewStatusName();
        }
        throw new ArgumentException($"Invalid Job status ID {status}");
    }
}