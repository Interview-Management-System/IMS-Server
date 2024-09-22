namespace InterviewManagementSystem.Domain.Enums;

public enum JobStatusEnum : byte
{
    Draft = 1,
    Open,
    Closed
}



public static class JobStatusEnumExtension
{
    private static readonly Dictionary<JobStatusEnum, string> JobStatusEnumMap = new()
    {
        {  JobStatusEnum.Draft, nameof(JobStatusEnum.Draft) },
        {  JobStatusEnum.Open, nameof(JobStatusEnum.Open) },
        {  JobStatusEnum.Closed, nameof(JobStatusEnum.Closed)  },
    };

    public static string GetJobStatusName(this JobStatusEnum status)
    {
        if (JobStatusEnumMap.TryGetValue(status, out string? name))
            return name.Trim();

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }
}