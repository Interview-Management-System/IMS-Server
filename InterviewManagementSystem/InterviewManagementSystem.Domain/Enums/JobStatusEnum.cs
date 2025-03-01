using System.ComponentModel;

namespace InterviewManagementSystem.Domain.Enums;

public enum JobStatusEnum
{
    Default,

    [Description("Draft")]
    Draft = 1,

    [Description("Open")]
    Open,

    [Description("Closed")]
    Closed
}

