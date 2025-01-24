using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Managers.UserManagers;

public sealed class CandidateManager : BaseUserManager
{

    UserManager<Candidate> test;


    public CandidateManager(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(mapper, unitOfWork, userManager, roleManager)
    {
    }




    public async Task<ApiResponse<CandidateForRetrieveDTO>> GetCandidateByIdAsync(Guid id)
    {
        var candidateFoundById = await _unitOfWork.CandidateRepository.GetByIdAsync(id);
        ArgumentNullException.ThrowIfNull(candidateFoundById, "Candidate not found");

        var mappedCandidate = _mapper.Map<CandidateForRetrieveDTO>(candidateFoundById);

        return new ApiResponse<CandidateForRetrieveDTO>()
        {
            Data = mappedCandidate,
            Message = "Candidate found"
        };
    }



    public async Task<string> SetCandidateStatus(Guid id, CandidateStatusEnum candidateStatusEnum)
    {
        var candidate = await test.FindByIdAsync(id.ToString());

        ArgumentNullException.ThrowIfNull(candidate, "User not found to de-activate");
        ApplicationException.ThrowIfInvalidOperation(candidate is not Candidate, "Not type of candidate");

        candidate = candidate as Candidate;

        CandidateException.ThrowIfSetTheSameStatus(candidate!.CandidateStatusId, candidateStatusEnum);

        candidate.SetCandidateStatus(candidateStatusEnum);

        var result = await _userManager.UpdateAsync(candidate);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Activate user failed");

        return "Change candidate status successfully";
    }



    public async Task<ApiResponse<List<CandidateForRetrieveDTO>>> GetCandidateListAsync()
    {

        var listUser = await _unitOfWork.CandidateRepository.GetAllAsync();
        var newListUser = _mapper.Map<List<CandidateForRetrieveDTO>>(listUser);


        foreach (var item in newListUser)
        {
            var userFoundById = await _userManager.FindByIdAsync(item.Id.ToString());
            var listRole = await _userManager.GetRolesAsync(userFoundById!);

            item.Role = listRole.FirstOrDefault();
        }


        return new ApiResponse<List<CandidateForRetrieveDTO>>()
        {
            Data = newListUser,
            Message = "Get user list successful"
        };
    }



    public async Task<string> CreateCandidateAsync(CandidateForCreateDTO candidateForCreateDTO)
    {

        var userFound = await _userManager.FindByEmailAsync(candidateForCreateDTO.Email.Trim());
        AppUserException.ThrowIfUserExist(userFound);


        AppRole? role = await _roleManager.FindByIdAsync(candidateForCreateDTO.RoleId.ToString()!);
        ArgumentNullException.ThrowIfNull(role, "Role not found to add user");


        var candidate = _mapper.Map<Candidate>(candidateForCreateDTO);
        candidate.Attachment = await FileUtility.ConvertFileToBytes(candidateForCreateDTO.Attachment);

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
        await UpdateUserRoleAsync(userFoundById, candidateForUpdateDTO.RoleId);



        var result = await _userManager.UpdateAsync(candidate!);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Update candidate failed");


        return "Update candidate successfully";
    }


}
