using InterviewManagementSystem.Application.DTOs.UserDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Application.Services;
using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Managers.UserManagers;

public sealed class UserManager : BaseUserManager
{

    public UserManager(
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        ICloudinaryService cloudinaryService
        ) : base(unitOfWork, userManager, roleManager, cloudinaryService)
    { }



    public async Task<List<UserIdentityRetrieveDTO>> GetListRecruiterAsync()
    {
        var recruiters = await _userManager.GetUsersInRoleAsync(nameof(RoleEnum.Recruiter));
        return MapperHelper.Map<List<UserIdentityRetrieveDTO>>(recruiters);
    }



    public async Task<List<UserIdentityRetrieveDTO>> GetListInterviewerAsync()
    {
        var interviewers = await _userManager.GetUsersInRoleAsync(nameof(RoleEnum.Interviewer));
        return MapperHelper.Map<List<UserIdentityRetrieveDTO>>(interviewers);
    }



    public async Task<string> CreateUserAsync(UserCreateDTO userForCreateDTO)
    {

        var userFound = await _userManager.FindByEmailAsync(userForCreateDTO.PersonalInformation.Email!.Trim());
        AppUserException.ThrowIfUserExist(userFound);


        AppRole? role = await _roleManager.FindByIdAsync(userForCreateDTO.RoleId.ToString());
        ArgumentNullException.ThrowIfNull(role, "Role not found to add user");


        var appUser = MapperHelper.Map<AppUser>(userForCreateDTO);


        var createUserResult = await _userManager.CreateAsync(appUser, DEFAULT_PASSWORD);
        ApplicationException.ThrowIfOperationFail(createUserResult.Succeeded, "Create user failed");


        var addRoleResult = await _userManager.AddToRoleAsync(appUser, role.Name!);
        ApplicationException.ThrowIfOperationFail(addRoleResult.Succeeded, "Create user failed");


        return "Create user successfully";
    }



    public async Task<ApiResponse<PageResult<UserPaginationRetrieveDTO>>> GetListUserPagingAsync(UserPaginatedSearchRequest request)
    {

        PaginationParameter<AppUser> paginationParameter = MapperHelper.Map<PaginationParameter<AppUser>>(request);

        RoleEnum roleId = request.RoleId;

        if (roleId.IsNotDefault())
        {
            AppRole? role = await _roleManager.FindByIdAsync(roleId.GetRoleId());
            ArgumentNullException.ThrowIfNull(role, "Role not found to filter");

            List<Guid> userWithRole = (await _userManager.GetUsersInRoleAsync(role.Name!)).Select(x => x.Id).ToList();
            paginationParameter.Filters.Add(x => userWithRole.Contains(x.Id));
        }


        var projection = MapperHelper.CreateProjection<AppUser, UserPaginationRetrieveDTO>();
        var pageResult = await _repository.GetPaginationList(paginationParameter, projection: projection);


        return new ApiResponse<PageResult<UserPaginationRetrieveDTO>>()
        {
            Data = MapperHelper.Map<PageResult<UserPaginationRetrieveDTO>>(pageResult)
        };
    }


    public async Task<string> UpdateUserAsync(UserUpdateDTO userForUpdateDTO)
    {

        var userFoundById = await _userManager.FindByIdAsync(userForUpdateDTO.Id.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to update");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        MapperHelper.Map(userForUpdateDTO, userFoundById);
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


        await base.UpdateUserRoleAsync(userFoundById, roleId);


        var result = await _userManager.UpdateAsync(userFoundById);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Change role failed");

        return "Change role successfully";
    }



    public async Task<string> DeActivateUser(Guid id)
    {
        bool updateSuccess = await _repository
            .InstantUpdateAsync(u => u.Id.Equals(id), u => u.SetProperty(u => u.IsActive, false));

        ApplicationException.ThrowIfOperationFail(updateSuccess, "De-activate user failed");
        return "De-activate user successfully";
    }



    public async Task<string> ActivateUser(Guid id)
    {
        bool updateSuccess = await _repository
            .InstantUpdateAsync(u => u.Id.Equals(id), u => u.SetProperty(u => u.IsActive, true));

        ApplicationException.ThrowIfOperationFail(updateSuccess, "Activate user failed");
        return "Activate user successfully";
    }
}
