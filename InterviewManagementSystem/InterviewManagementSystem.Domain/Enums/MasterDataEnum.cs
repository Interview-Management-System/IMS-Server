using System.ComponentModel;

namespace InterviewManagementSystem.Domain.Enums;

public enum SkillsEnum
{
    Default,

    [Description("Java")]
    Java = 1,

    [Description("NodeJs")]
    NodeJs,

    [Description(".NET")]
    DotNet,

    [Description("C++")]
    CPlus,

    [Description("Business Analysis")]
    BusinessAnalysis,

    [Description("Communication")]
    Communication,
}



public enum PositionEnum
{
    Default,

    [Description("Backend Developer")]
    BackendDeveloper = 1,

    [Description("Business Analyst")]
    BusinessAnalyst,

    [Description("Tester")]
    Tester,

    [Description("Human Resource")]
    HR,

    [Description("Project Manager")]
    ProjectManager,

    [Description("Not Available")]
    NotAvailable
}



public enum LevelEnum
{
    Default,

    [Description("Fresher")]
    Fresher = 1,

    [Description("Junior")]
    Junior,

    [Description("Senior")]
    Senior,

    [Description("Leader")]
    Leader,

    [Description("Manager")]
    Manager,

    [Description("ViceHead")]
    ViceHead,
}


public enum HighestLevelEnum
{
    Default,

    [Description("High School")]
    HighSchool = 1,

    [Description("Bachelor Degree")]
    BachelorDegree,

    [Description("Master Degree")]
    MasterDegree,

    [Description("Doctor of Philosophy")]
    PhD
}


public enum DepartmentEnum
{
    Default,

    [Description("IT")]
    IT = 1,

    [Description("HR")]
    HR,

    [Description("Finance")]
    Finance,

    [Description("Communication")]
    Communication,

    [Description("Marketing")]
    Marketing,

    [Description("Accounting")]
    Accounting,
}




public enum BenefitEnum
{
    Default,

    [Description("Travel")]
    Travel = 1,

    [Description("Hybrid Working")]
    HybridWorking,

    [Description("Healthcare Insurance")]
    HealthcareInsurance,

    [Description("Day Leave 25")]
    DayLeave25,

    [Description("Lunch")]
    Lunch,
}



public enum ContractTypeEnum
{
    Default,

    [Description("Trial 2 Months")]
    Trial2Months = 1,

    [Description("Trainee 3 Months")]
    Trainee3Months,

    [Description("1 Year")]
    OneYear,

    [Description("3 Years")]
    ThreeYears,

    [Description("Unlimited")]
    Unlimited
}

