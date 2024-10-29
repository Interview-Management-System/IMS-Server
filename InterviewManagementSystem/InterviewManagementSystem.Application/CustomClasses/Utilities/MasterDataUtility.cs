using InterviewManagementSystem.Domain.Entities.MasterData;

namespace InterviewManagementSystem.Application.CustomClasses.Utilities;

internal static class MasterDataUtility
{

    public static IUnitOfWork UnitOfWork { get; internal set; } = default!;



    internal static async Task<List<Skill>> GetListSkillByIdList(SkillsEnum[] skillsEnums)
    {
        return await UnitOfWork
              .GetBaseRepository<Skill>()
              .GetAllAsync(s => skillsEnums.Select(s => (short)s).Contains(s.Id), true);
    }


    internal static async Task<List<Level>> GetListLevelByIdList(LevelEnum[] levelEnums)
    {
        return await UnitOfWork
              .GetBaseRepository<Level>()
              .GetAllAsync(s => levelEnums.Select(s => (short)s).Contains(s.Id), true);
    }


    internal static async Task<List<Benefit>> GetListBenefitByIdList(BenefitEnum[] benefitEnums)
    {
        return await UnitOfWork
              .GetBaseRepository<Benefit>()
              .GetAllAsync(s => benefitEnums.Select(s => (short)s).Contains(s.Id), true);
    }
}
