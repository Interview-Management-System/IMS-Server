namespace InterviewManagementSystem.Application.Shared.Utilities;

public static class MasterDataUtility
{

    private static IUnitOfWork? _unitOfWork;



    public static void InitializeUnitOfWorkInstance(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public static async Task<List<Skill>> GetListSkillByIdListAsync(SkillsEnum[] skillsEnums)
    {
        return await _unitOfWork!
            .SkillRepository
            .GetAllAsync<Skill>(s => skillsEnums.Length > 0 && skillsEnums.Contains(s.Id), isTracking: true);
    }



    internal static async Task<List<Level>> GetListLevelByIdListAsync(LevelEnum[] levelEnums)
    {
        return await _unitOfWork!
              .LevelRepository
              .GetAllAsync<Level>(s => levelEnums.Length > 0 && levelEnums.Contains(s.Id), isTracking: true);
    }


    internal static async Task<List<Benefit>> GetListBenefitByIdListAsync(BenefitEnum[] benefitEnums)
    {
        return await _unitOfWork!
              .BenefitRepository
              .GetAllAsync<Benefit>(s => benefitEnums.Length > 0 && benefitEnums.Contains(s.Id), isTracking: true);
    }
}
