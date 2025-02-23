using InterviewManagementSystem.Application.DTOs.UserDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Application.Services;
using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Managers.UserManagers;

public sealed class UserManager : BaseUserManager
{

    public UserManager(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        ICloudinaryService cloudinaryService
        ) : base(mapper, unitOfWork, userManager, roleManager, cloudinaryService)
    { }



    public async Task<List<UserIdentityRetrieveDTO>> GetListRecruiterAsync()
    {
        var recruiters = await _userManager.GetUsersInRoleAsync(nameof(RoleEnum.Recruiter));
        return _mapper.Map<List<UserIdentityRetrieveDTO>>(recruiters);
    }



    public async Task<List<UserIdentityRetrieveDTO>> GetListInterviewerAsync()
    {
        var interviewers = await _userManager.GetUsersInRoleAsync(nameof(RoleEnum.Interviewer));
        return _mapper.Map<List<UserIdentityRetrieveDTO>>(interviewers);
    }



    public async Task<string> CreateUserAsync(UserCreateDTO userForCreateDTO)
    {

        var userFound = await _userManager.FindByEmailAsync(userForCreateDTO.PersonalInformation.Email!.Trim());
        AppUserException.ThrowIfUserExist(userFound);


        AppRole? role = await _roleManager.FindByIdAsync(userForCreateDTO.RoleId.ToString());
        ArgumentNullException.ThrowIfNull(role, "Role not found to add user");


        var appUser = _mapper.Map<AppUser>(userForCreateDTO);


        var createUserResult = await _userManager.CreateAsync(appUser, DEFAULT_PASSWORD);
        ApplicationException.ThrowIfOperationFail(createUserResult.Succeeded, "Create user failed");


        var addRoleResult = await _userManager.AddToRoleAsync(appUser, role.Name!);
        ApplicationException.ThrowIfOperationFail(addRoleResult.Succeeded, "Create user failed");


        return "Create user successfully";
    }



    public async Task<ApiResponse<PageResult<UserPaginationRetrieveDTO>>> GetListUserPagingAsync(UserPaginatedSearchRequest request)
    {

        PaginationParameter<AppUser> paginationParameter = _mapper.Map<PaginationParameter<AppUser>>(request);

        RoleEnum roleId = request.RoleId;

        if (roleId.IsNotDefault())
        {
            AppRole? role = await _roleManager.FindByIdAsync(roleId.GetRoleId());
            ArgumentNullException.ThrowIfNull(role, "Role not found to filter");

            List<Guid> userWithRole = (await _userManager.GetUsersInRoleAsync(role.Name!)).Select(x => x.Id).ToList();
            paginationParameter.Filters.Add(x => userWithRole.Contains(x.Id));
        }


        var projection = MapperHelper.CreateProjection<AppUser, UserPaginationRetrieveDTO>(_mapper);
        var pageResult = await _repository.GetPaginationList(paginationParameter, projection: projection);


        return new ApiResponse<PageResult<UserPaginationRetrieveDTO>>()
        {
            Data = _mapper.Map<PageResult<UserPaginationRetrieveDTO>>(pageResult)
        };
    }


    public async Task<string> UpdateUserAsync(UserUpdateDTO userForUpdateDTO)
    {

        var userFoundById = await _userManager.FindByIdAsync(userForUpdateDTO.Id.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to update");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        _mapper.Map(userForUpdateDTO, userFoundById);
        await UpdateUserRoleAsync(userFoundById, userForUpdateDTO.RoleId);


        var result = await _userManager.UpdateAsync(userFoundById);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Update failed");


        return "Update user successfully";
    }



    public async Task<string> ChangeUserRole(Guid userId, Guid roleId)
    {

        var userFoundById = await _userManager.FindByIdAsync(userId.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to assign new role");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        await UpdateUserRoleAsync(userFoundById, roleId);


        var result = await _userManager.UpdateAsync(userFoundById);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Change role failed");

        return "Change role successfully";
    }



    public async Task<string> DeActivateUser(Guid id)
    {
        var userFoundById = await _userManager.FindByIdAsync(id.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to de-activate");
        AppUserException.ThrowIfUserNotActive(userFoundById);


        userFoundById.DeActivate();
        var result = await _userManager.UpdateAsync(userFoundById);


        ApplicationException.ThrowIfOperationFail(result.Succeeded, "De-activate user failed");
        return "De-activate user successfully";
    }




    public async Task<string> ActivateUser(Guid id)
    {
        var userFoundById = await _userManager.FindByIdAsync(id.ToString());
        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to de-activate");

        userFoundById.Activate();
        var result = await _userManager.UpdateAsync(userFoundById);

        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Activate user failed");
        return "Activate user successfully";
    }
}
