namespace InterviewManagementSystem.Application.Shared.Utilities;

internal static class MasterDataUtility
{

    public static IUnitOfWork UnitOfWork { get; internal set; } = default!;



    internal static async Task<List<Skill>> GetListSkillByIdListAsync(SkillsEnum[] skillsEnums)
    {
        return await UnitOfWork
              .GetBaseRepository<Skill>()
              .GetAllAsync(s => skillsEnums.Length > 0 && skillsEnums.Contains(s.Id), true);
    }


    internal static async Task<List<Level>> GetListLevelByIdListAsync(LevelEnum[] levelEnums)
    {
        return await UnitOfWork
              .GetBaseRepository<Level>()
              .GetAllAsync(s => levelEnums.Length > 0 && levelEnums.Contains(s.Id), true);
    }


    internal static async Task<List<Benefit>> GetListBenefitByIdListAsync(BenefitEnum[] benefitEnums)
    {
        return await UnitOfWork
              .GetBaseRepository<Benefit>()
              .GetAllAsync(s => benefitEnums.Length > 0 && benefitEnums.Contains(s.Id), true);
    }
}
