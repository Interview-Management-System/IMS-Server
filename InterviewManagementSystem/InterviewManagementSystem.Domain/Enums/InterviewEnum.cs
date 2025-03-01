using System.ComponentModel;

namespace InterviewManagementSystem.Domain.Enums;

public enum InterviewResultEnum
{
    Default,

    [Description("Open")]
    Open,

    [Description("Passed")]
    Pass,

    [Description("Failed")]
    Failed,

    [Description("N/A")]
    NA
}


public enum InterviewStatusEnum
{
    Default,

    [Description("New")]
    New = 1,

    [Description("Invited")]
    Invited,

    [Description("Interviewed")]
    Interviewed,

    [Description("Cancelled")]
    Cancelled,

    [Description("Closed")]
    Closed,
}
