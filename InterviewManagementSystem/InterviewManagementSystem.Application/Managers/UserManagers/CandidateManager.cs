using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Managers.UserManagers;

public sealed class CandidateManager : BaseUserManager
{

    private readonly UserManager<AppUser> _candidateManager;
    private readonly IBaseRepository<Candidate> _candidateRepository;

    public CandidateManager(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> candidateManager, RoleManager<AppRole> roleManager) : base(mapper, unitOfWork, candidateManager, roleManager)
    {
        _candidateManager = candidateManager;
        _candidateRepository = unitOfWork.CandidateRepository;
    }




    public async Task<ApiResponse<PageResult<CandidateForPaginationRetrieveDTO>>> GetListCandidatePagingAsync(CandidatePaginatedSearchRequest request)
    {

        PaginationParameter<Candidate> paginationParameter = _mapper.Map<PaginationParameter<Candidate>>(request);


        CandidateStatusEnum statusId = request.StatusId;

        if (statusId.IsNotDefault())
        {
            paginationParameter.Filters.Add(f => f.CandidateStatusId == statusId);
        }


        var projection = MapperHelper.CreateProjection<Candidate, CandidateForPaginationRetrieveDTO>(_mapper);
        var pageResult = await _candidateRepository.GetPaginationList(paginationParameter, projection: projection);


        return new ApiResponse<PageResult<CandidateForPaginationRetrieveDTO>>()
        {
            Data = _mapper.Map<PageResult<CandidateForPaginationRetrieveDTO>>(pageResult)
        };
    }




    public async Task<ApiResponse<CandidateForDetailRetrieveDTO>> GetCandidateByIdAsync(Guid id)
    {

        var projection = MapperHelper.CreateProjection<Candidate, CandidateForDetailRetrieveDTO>(_mapper);

        var candidateFoundById = await _candidateRepository.GetByIdAsync(id, projection: projection);
        ArgumentNullException.ThrowIfNull(candidateFoundById, "Candidate not found");

        var mappedCandidate = _mapper.Map<CandidateForDetailRetrieveDTO>(candidateFoundById);

        return new ApiResponse<CandidateForDetailRetrieveDTO>()
        {
            Data = mappedCandidate
        };
    }



    public async Task<string> SetCandidateStatus(Guid id, CandidateStatusEnum candidateStatusEnum)
    {
        var candidate = await _userManager.FindByIdAsync(id.ToString());

        ArgumentNullException.ThrowIfNull(candidate, "User not found to de-activate");
        ApplicationException.ThrowIfInvalidOperation(candidate is not Candidate, "Not type of candidate");

        candidate = candidate as Candidate;

        //CandidateException.ThrowIfSetTheSameStatus(candidate!.CandidateStatusId, candidateStatusEnum);

        //candidate.SetCandidateStatus(candidateStatusEnum);

        var result = await _userManager.UpdateAsync(candidate);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Activate user failed");

        return "Change candidate status successfully";
    }


    /*
    public async Task<ApiResponse<List<CandidateForRetrieveDTO>>> GetCandidateListAsync()
    {

        var listUser = await _candidateRepository.GetAllAsync();
        var newListUser = _mapper.Map<List<CandidateForRetrieveDTO>>(listUser);


        foreach (var item in newListUser)
        {
            var userFoundById = await _userManager.FindByIdAsync(item.Id.ToString());
            var listRole = await _userManager.GetRolesAsync(userFoundById!);

            item. = listRole.FirstOrDefault();
        }


        return new ApiResponse<List<CandidateForRetrieveDTO>>()
        {
            Data = newListUser,
            Message = "Get user list successful"
        };
    }
    */


    public async Task<string> CreateCandidateAsync(CandidateForCreateDTO candidateForCreateDTO)
    {

        var userFound = await _userManager.FindByEmailAsync(candidateForCreateDTO.PersonalInformation.Email!.Trim());
        AppUserException.ThrowIfUserExist(userFound);

        AppRole? role = await _roleManager.FindByIdAsync(RoleEnum.Candidate.GetRoleId());
        ArgumentNullException.ThrowIfNull(role, "Role not found to add user");


        var candidate = _mapper.Map<Candidate>(candidateForCreateDTO);


        var skills = await MasterDataUtility.GetListSkillByIdListAsync(candidateForCreateDTO.SkillList, _unitOfWork);
        candidate.SetSkillList(skills);


        var createUserResult = await _userManager.CreateAsync(candidate, DEFAULT_PASSWORD);
        ApplicationException.ThrowIfOperationFail(createUserResult.Succeeded, "Create candidate failed");


        var addRoleResult = await _userManager.AddToRoleAsync(candidate, role.Name!.Trim());
        ApplicationException.ThrowIfOperationFail(addRoleResult.Succeeded, "Set role failed");


        return "Create candidate successfully";
    }


    public async Task<string> UpdateCandidateAsync(CandidateForUpdateDTO candidateForUpdateDTO)
    {

        var userFoundById = await _userManager.FindByIdAsync(candidateForUpdateDTO.Id.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to update");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        if (userFoundById is not Candidate)
            throw new InvalidOperationException("Not type of Candidate");


        var candidate = userFoundById as Candidate;


        _mapper.Map(candidateForUpdateDTO, candidate);
        //await UpdateUserRoleAsync(userFoundById, candidateForUpdateDTO.RoleId);



        var result = await _userManager.UpdateAsync(candidate!);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Update candidate failed");


        return "Update candidate successfully";
    }


}
