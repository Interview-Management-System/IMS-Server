namespace InterviewManagementSystem.Domain.Enums;

public enum ContractTypeEnum : short
{
    Trial2Months = 1,
    Trainee3Months,
    OneYear,
    ThreeYears,
    Unlimited
}




public static class ContractTypeEnumExtension
{
    private static readonly Dictionary<ContractTypeEnum, string> ContractTypeEnumMap = new()
    {
        {  ContractTypeEnum.Trial2Months, "Trial 2 months" },
        {  ContractTypeEnum.Trainee3Months, "Trainee 3 months" },
        {  ContractTypeEnum.OneYear, "1 Year" },
        {  ContractTypeEnum.ThreeYears, "3 Year" },
        {  ContractTypeEnum.Unlimited, "Unlimited"},
    };

    public static string GetContractTypeName(this ContractTypeEnum status)
    {
        if (ContractTypeEnumMap.TryGetValue(status, out string? name))
            return name;

        throw new ArgumentException($"No GUID mapping found for status {status}");
    }


    public static string GetContractTypeNameById(this short status)
    {
        if (Enum.IsDefined(typeof(ContractTypeEnum), status))
        {
            var contractType = (ContractTypeEnum)status;
            return contractType.GetContractTypeName();
        }
        throw new ArgumentException($"Invalid Job status ID {status}");
    }
}

