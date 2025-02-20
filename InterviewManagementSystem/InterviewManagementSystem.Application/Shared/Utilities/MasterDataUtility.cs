namespace InterviewManagementSystem.Application.Shared.Utilities;

internal static class MasterDataUtility
{

    internal static async Task<List<Skill>> GetListSkillByIdListAsync(SkillsEnum[] skillsEnums, IUnitOfWork unitOfWork)
    {
        return await unitOfWork
              .SkillRepository
              .GetAllAsync(s => skillsEnums.Length > 0 && skillsEnums.Contains(s.Id), true);
    }


    internal static async Task<List<Level>> GetListLevelByIdListAsync(LevelEnum[] levelEnums, IUnitOfWork unitOfWork)
    {
        return await unitOfWork
              .LevelRepository
              .GetAllAsync(s => levelEnums.Length > 0 && levelEnums.Contains(s.Id), true);
    }


    internal static async Task<List<Benefit>> GetListBenefitByIdListAsync(BenefitEnum[] benefitEnums, IUnitOfWork unitOfWork)
    {
        return await unitOfWork
              .BenefitRepository
              .GetAllAsync(s => benefitEnums.Length > 0 && benefitEnums.Contains(s.Id), true);
    }
}
