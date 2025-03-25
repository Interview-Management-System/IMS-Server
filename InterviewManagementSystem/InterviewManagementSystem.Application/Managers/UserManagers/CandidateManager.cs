using InterviewManagementSystem.Application.DTOs.UserDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.Services;
using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Enums.Helpers;

namespace InterviewManagementSystem.Application.Managers.UserManagers;

public sealed class CandidateManager : BaseUserManager
{
    private readonly UserManager<AppUser> _candidateManager;
    private readonly IBaseRepository<Candidate> _candidateRepository;


    public CandidateManager(
        IUnitOfWork unitOfWork,
        UserManager<AppUser> candidateManager,
        RoleManager<AppRole> roleManager,
        ICloudinaryService cloudinaryService
        ) : base(unitOfWork, candidateManager, roleManager, cloudinaryService)
    {
        _candidateManager = candidateManager;
        _candidateRepository = unitOfWork.CandidateRepository;
    }



    public async Task<List<UserIdentityRetrieveDTO>> GetListCandidateForInterviewAsync()
    {

        var allowedCandidateToInterview = CandidateEnumHelper.AllowedCandidateStatusForInterview;
        var projection = MapperHelper.CreateProjection<Candidate, UserIdentityRetrieveDTO>();


        var candidateForInterview = await _candidateRepository
            .GetAllAsync(c => allowedCandidateToInterview.Contains(c.CandidateStatusId!.Value), projection: projection);

        return candidateForInterview;
    }




    public async Task<ApiResponse<PageResult<CandidatePaginationRetrieveDTO>>> GetListCandidatePagingAsync(CandidatePaginatedSearchRequest request)
    {

        PaginationParameter<Candidate> paginationParameter = MapperHelper.Map<PaginationParameter<Candidate>>(request);


        CandidateStatusEnum statusId = request.StatusId;

        if (statusId.IsNotDefault())
        {
            paginationParameter.Filters.Add(f => f.CandidateStatusId == statusId);
        }


        var projection = MapperHelper.CreateProjection<Candidate, CandidatePaginationRetrieveDTO>();
        var pageResult = await _candidateRepository.GetPaginationList(paginationParameter, projection: projection);


        return new ApiResponse<PageResult<CandidatePaginationRetrieveDTO>>()
        {
            Data = MapperHelper.Map<PageResult<CandidatePaginationRetrieveDTO>>(pageResult)
        };
    }




    public override async Task<ApiResponse<TDetailDTO>> GetDetailByIdAsync<TDetailDTO>(object id)
    {
        var projection = MapperHelper.CreateProjection<Candidate, TDetailDTO>();

        var candidateFoundById = await _candidateRepository.GetByIdAsync(id, projection: projection);
        ArgumentNullException.ThrowIfNull(candidateFoundById, "Candidate not found");

        var mappedCandidate = MapperHelper.Map<TDetailDTO>(candidateFoundById);

        return new ApiResponse<TDetailDTO>()
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


    public async Task<string> CreateCandidateAsync(CandidateCreateDTO candidateForCreateDTO)
    {

        var userFound = await _userManager.FindByEmailAsync(candidateForCreateDTO.PersonalInformation.Email!.Trim());
        AppUserException.ThrowIfUserExist(userFound);

        AppRole? role = await _roleManager.FindByIdAsync(RoleEnum.Candidate.GetRoleId());
        ArgumentNullException.ThrowIfNull(role, "Role not found to add user");


        var candidate = MapperHelper.Map<Candidate>(candidateForCreateDTO);


        var skills = await MasterDataUtility.GetListSkillByIdListAsync(candidateForCreateDTO.SkillList);
        candidate.SetSkillList(skills);


        var uploadAvatarTask = _cloudinaryService.UploadFileAsync(candidateForCreateDTO.Avatar);
        var uploadAttachmentTask = _cloudinaryService.UploadFileAsync(candidateForCreateDTO.Attachment);

        await Task.WhenAll(uploadAvatarTask, uploadAttachmentTask);

        candidate.AvatarLink = uploadAvatarTask?.Result?.SecureUrl.ToString();
        candidate.AttachmentLink = uploadAttachmentTask?.Result?.SecureUrl.ToString();


        var createUserResult = await _userManager.CreateAsync(candidate, DEFAULT_PASSWORD);
        ApplicationException.ThrowIfOperationFail(createUserResult.Succeeded, "Create candidate failed");


        var addRoleResult = await _userManager.AddToRoleAsync(candidate, role.Name!.Trim());
        ApplicationException.ThrowIfOperationFail(addRoleResult.Succeeded, "Set role failed");


        return "Create candidate successfully";
    }


    public async Task<string> UpdateCandidateAsync(CandidateUpdateDTO candidateForUpdateDTO)
    {

        var userFoundById = await _userManager.FindByIdAsync(candidateForUpdateDTO.Id.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to update");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        if (userFoundById is not Candidate)
            throw new InvalidOperationException("Not type of Candidate");


        var candidate = userFoundById as Candidate;


        MapperHelper.Map(candidateForUpdateDTO, candidate);
        //await UpdateUserRoleAsync(userFoundById, candidateForUpdateDTO.RoleId);



        var result = await _userManager.UpdateAsync(candidate!);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Update candidate failed");


        return "Update candidate successfully";
    }


}
