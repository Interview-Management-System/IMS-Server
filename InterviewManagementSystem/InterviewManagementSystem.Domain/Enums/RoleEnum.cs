using System.ComponentModel;

namespace InterviewManagementSystem.Domain.Enums;

public enum RoleEnum
{
    Default = 0,

    [Description("Admin")]
    Admin = 1,

    [Description("Manager")]
    Manager,

    [Description("Candidate")]
    Candidate,

    [Description("Recruiter")]
    Recruiter,

    [Description("Interviewer")]
    Interviewer,
}