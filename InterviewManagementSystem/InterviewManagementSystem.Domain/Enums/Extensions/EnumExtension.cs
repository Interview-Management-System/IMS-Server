namespace InterviewManagementSystem.Domain.Enums.Extensions;

public static class EnumExtension
{

    private static readonly Dictionary<RoleEnum, string> RoleIdEnumMap = new()
    {
        { RoleEnum.Admin, "5900eab6-e2e1-4160-83ee-b985ac709f46" },
        { RoleEnum.Candidate, "83926601-feef-4371-8fbe-40a68fbe8ef6" },
        { RoleEnum.Manager, "60de2025-9b80-4ce2-920c-4628860ccfce" },
        { RoleEnum.Recruiter, "b13ce32b-05a7-480b-817f-099106c463a7" },
        { RoleEnum.Interviewer, "9360eb9d-3aa2-4448-ba57-920cb69043af" },
    };


    // Centralized dictionary to store mappings for multiple enums
    private static readonly Dictionary<Type, Dictionary<Enum, string>> EnumMappings = new()
    {
        {
            typeof(CandidateStatusEnum), new Dictionary<Enum, string>
            {
                { CandidateStatusEnum.Open, "Open" },
                { CandidateStatusEnum.Banned, "Banned" },
                { CandidateStatusEnum.WaitingForInterview, "Waiting For Interview" },
                { CandidateStatusEnum.InProgress, "In Progress" },
                { CandidateStatusEnum.Cancelled, "Cancelled" },
                { CandidateStatusEnum.FailedInterview, "Failed Interview" },
                { CandidateStatusEnum.PassedInterview, "Passed Interview" },
                { CandidateStatusEnum.WaitingForApproval, "Waiting For Approval" },
                { CandidateStatusEnum.RejectedOffer, "Rejected Offer" },
                { CandidateStatusEnum.ApprovedOffer, "Approved Offer" },
                { CandidateStatusEnum.CancelledOffer, "Cancelled Offer" },
                { CandidateStatusEnum.WaitingForResponse, "Waiting For Response" },
                { CandidateStatusEnum.AcceptedOffer, "Accepted Offer" },
                { CandidateStatusEnum.DeclinedOffer, "Declined Offer" }
            }
        },

        {
            typeof(BenefitEnum), new Dictionary<Enum, string>
            {
                {  BenefitEnum.Lunch, "Lunch"},
                {  BenefitEnum.DayLeave25, "25 Day Leave" },
                {  BenefitEnum.HealthcareInsurance, "Healthcare Insurance" },
                {  BenefitEnum.HybridWorking, "Hybrid Working" },
                {  BenefitEnum.Travel, "Travel" },
            }
        },

        {
            typeof(ContractTypeEnum), new Dictionary<Enum, string>
            {
                {  ContractTypeEnum.Trial2Months, "Trial 2 months" },
                {  ContractTypeEnum.Trainee3Months, "Trainee 3 months" },
                {  ContractTypeEnum.OneYear, "1 Year" },
                {  ContractTypeEnum.ThreeYears, "3 Year" },
                {  ContractTypeEnum.Unlimited, "Unlimited"},
            }
        },

        {
            typeof(DepartmentEnum), new Dictionary<Enum, string>
            {
                {  DepartmentEnum.IT, "IT" },
                {  DepartmentEnum.HR, "Human Resource" },
                {  DepartmentEnum.Finance, "Finance" },
                {  DepartmentEnum.Communication, "Communication" },
                {  DepartmentEnum.Marketing, "Marketing"},
                {  DepartmentEnum.Accounting, "Accounting" },
            }
        },

        {
            typeof(HighestLevelEnum), new Dictionary<Enum, string>
            {
                {  HighestLevelEnum.HighSchool, "High School" },
                {  HighestLevelEnum.BachelorDegree, "Bachelor Degree" },
                {  HighestLevelEnum.MasterDegree, "Master Degree" },
                {  HighestLevelEnum.PhD, "Doctor of Philosophy" },
            }
        },


        {
            typeof(InterviewResultEnum), new Dictionary<Enum, string>
            {
                {  InterviewResultEnum.Open, "Open"},
                {  InterviewResultEnum.Pass, "Pass"},
                {  InterviewResultEnum.Failed, "Failed"},
                {  InterviewResultEnum.NA, "N/A"},
            }
        },

        {
            typeof(InterviewStatusEnum), new Dictionary<Enum, string>
            {
                {  InterviewStatusEnum.New, "New"},
                {  InterviewStatusEnum.Invited, "Invited"},
                {  InterviewStatusEnum.Interviewed, "Interviewed"},
                {  InterviewStatusEnum.Cancelled, "Cancelled"},
            }
        },

        {
            typeof(JobStatusEnum), new Dictionary<Enum, string>
            {
                {  JobStatusEnum.Draft, nameof(JobStatusEnum.Draft) },
                {  JobStatusEnum.Open, nameof(JobStatusEnum.Open) },
                {  JobStatusEnum.Closed, nameof(JobStatusEnum.Closed)  },
            }
        },

        {
            typeof(LevelEnum), new Dictionary<Enum, string>
            {
                {  LevelEnum.Fresher, "Fresher" },
                {  LevelEnum.Junior, "Junior" },
                {  LevelEnum.Senior, "Senior" },
                {  LevelEnum.Leader, "Leader" },
                {  LevelEnum.Manager, "Manager"},
                {  LevelEnum.ViceHead, "Vice Head" },
            }
        },

        {
            typeof(OfferStatusEnum), new Dictionary<Enum, string>
            {
                {  OfferStatusEnum.WaitingForApproval, "Waiting for approval" },
                {  OfferStatusEnum.Approved, nameof(OfferStatusEnum.Approved) },
                {  OfferStatusEnum.Rejected, nameof(OfferStatusEnum.Rejected)  },
                {  OfferStatusEnum.WaitingForResponse, "Waiting for response" },
                {  OfferStatusEnum.Cancelled, nameof(OfferStatusEnum.Cancelled) },
                {  OfferStatusEnum.Accepted, nameof(OfferStatusEnum.Accepted)  },
                {  OfferStatusEnum.Declined, nameof(OfferStatusEnum.Declined)  },
            }
        },

        {
            typeof(PositionEnum), new Dictionary<Enum, string>
            {
                {  PositionEnum.BackendDeveloper, "Backend Developer" },
                {  PositionEnum.BusinessAnalyst, "Business Analyst" },
                {  PositionEnum.Tester, "Tester" },
                {  PositionEnum.HR, "Human Resource" },
                {  PositionEnum.ProjectManager, "Project Manager"},
                {  PositionEnum.NotAvailable, "Not Available" },
            }
        },

        {
            typeof(RoleEnum), new Dictionary<Enum, string>
            {
                {  RoleEnum.Admin, "Admin" },
                {  RoleEnum.Candidate, "Candidate" },
                {  RoleEnum.Manager, "Manager" },
                {  RoleEnum.Recruiter, "Recruiter" },
                {  RoleEnum.Interviewer, "Interviewer" },
            }
        },

        {
            typeof(SkillsEnum), new Dictionary<Enum, string>
            {
                {  SkillsEnum.Java, "Java" },
                {  SkillsEnum.NodeJs, "NodeJs" },
                {  SkillsEnum.DotNet, ".NET" },
                {  SkillsEnum.CPlus, "C++" },
                {  SkillsEnum.BusinessAnalysis, "Business Analysis"},
                {  SkillsEnum.Communication, "Communication" },
            }
        },
    };


    // Generic method to get the name of any enum value
    public static string GetEnumName<TEnum>(this TEnum enumValue) where TEnum : Enum
    {
        var enumType = typeof(TEnum);

        bool isEnumExist = EnumMappings.TryGetValue(enumType, out var mappings);
        bool isEnumHasValue = mappings!.TryGetValue(enumValue, out var name);

        return isEnumExist && isEnumHasValue ? name?.Trim() ?? "" : "";
        throw new ArgumentException($"No mapping found for enum value '{enumValue}' in type '{enumType.Name}'");
    }



    // Get RoleId for RoleEnum
    public static string GetRoleId(this RoleEnum role)
    {
        if (RoleIdEnumMap.TryGetValue(role, out var id))
        {
            return id;
        }

        throw new ArgumentException($"No GUID mapping found for role {role}");
    }

    // Get RoleName by RoleId (GUID)
    public static string GetRoleNameById(this Guid roleId)
    {
        foreach (var (roleEnum, guidString) in RoleIdEnumMap)
        {
            if (Guid.TryParse(guidString, out var guid) && guid == roleId)
            {
                return roleEnum.GetEnumName();
            }
        }

        throw new ArgumentException($"Invalid role ID {roleId}");
    }
}
