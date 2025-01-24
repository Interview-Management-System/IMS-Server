﻿using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Enums.Extensions;
using InterviewManagementSystem.Domain.Shared.Paginations;

namespace InterviewManagementSystem.Application.Managers.UserManagers;

public sealed class UserManager : BaseUserManager
{


    public UserManager(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(mapper, unitOfWork, userManager, roleManager)
    {
    }



    public async Task<ApiResponse<List<UserForRetrieveDTO>>> GetListAsync()
    {

        var listUser = await _userManager.Users.ToListAsync();
        var newListUser = _mapper.Map<List<UserForRetrieveDTO>>(listUser);


        foreach (var item in newListUser)
        {
            var userFoundById = await _userManager.FindByIdAsync(item.Id.ToString());
            var listRole = await _userManager.GetRolesAsync(userFoundById!);

            item.Role = listRole.FirstOrDefault();
        }


        return new ApiResponse<List<UserForRetrieveDTO>>()
        {
            Data = newListUser,
            Message = "Get user list successful"
        };
    }




    public async Task<string> CreateUserAsync(UserForCreateDTO userForCreateDTO)
    {

        var userFound = await _userManager.FindByEmailAsync(userForCreateDTO.Email.Trim());
        AppUserException.ThrowIfUserExist(userFound);


        AppRole? role = await _roleManager.FindByIdAsync(userForCreateDTO.RoleId.ToString()!);
        ArgumentNullException.ThrowIfNull(role, "Role not found to add user");


        var appUser = _mapper.Map<AppUser>(userForCreateDTO);


        var createUserResult = await _userManager.CreateAsync(appUser, DEFAULT_PASSWORD);
        ApplicationException.ThrowIfOperationFail(createUserResult.Succeeded, "Create user failed");


        var addRoleResult = await _userManager.AddToRoleAsync(appUser, role.Name!);
        ApplicationException.ThrowIfOperationFail(addRoleResult.Succeeded, "Create user failed");


        return "Create user successfully";
    }



    public async Task<ApiResponse<PageResult<UserForRetrieveDTO>>> GetListUserPagingAsync(UserPaginatedSearchRequest request)
    {

        PaginationParameter<AppUser> paginationParameter = _mapper.Map<PaginationParameter<AppUser>>(request);


        if (request!.IsLoadDeleted == false)
        {
            paginationParameter.Filters.Add(f => !f.IsDeleted);
        }




        RoleEnum? roleId = request.RoleId;

        if (roleId != null && roleId != RoleEnum.Default)
        {

            AppRole? role = await _roleManager.FindByIdAsync(roleId.Value.GetRoleId());
            ArgumentNullException.ThrowIfNull(role, "Role not found to filter");

            List<Guid> userWithRole = (await _userManager.GetUsersInRoleAsync(role.Name!)).Select(x => x.Id).ToList();
            paginationParameter.Filters.Add(x => userWithRole.Contains(x.Id));
        }


        var projection = MapperHelper.CreateProjection<AppUser, UserForRetrieveDTO>(_mapper);
        var pageResult = await _unitOfWork.AppUserRepository.GetPaginationList(paginationParameter, projection: projection);


        return new ApiResponse<PageResult<UserForRetrieveDTO>>()
        {
            Data = _mapper.Map<PageResult<UserForRetrieveDTO>>(pageResult)
        };
    }



    public async Task<ApiResponse<UserForRetrieveDTO>> GetUserByIdAsync(Guid id)
    {

        var userFoundById = await _unitOfWork.AppUserRepository.GetByIdAsync(id);

        ArgumentNullException.ThrowIfNull(userFoundById, "User not found");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        var mappedUser = _mapper.Map<UserForRetrieveDTO>(userFoundById);
        var listRole = await _userManager.GetRolesAsync(userFoundById);

        mappedUser.Role = listRole.FirstOrDefault();


        return new ApiResponse<UserForRetrieveDTO>()
        {
            Data = mappedUser
        };
    }



    public async Task<string> UpdateUserAsync(UserForUpdateDTO userForUpdateDTO)
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




    public async Task<string> DeleteUserAsync(Guid id)
    {

        var userFoundById = await _userManager.FindByIdAsync(id.ToString());

        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to delete");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        userFoundById.Delete();


        var result = await _userManager.UpdateAsync(userFoundById);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Delete user failed");


        return "Update user successfully";
    }




    public async Task<string> UnDoDeleteUserAsync(Guid id)
    {

        var userFoundById = await _userManager.FindByIdAsync(id.ToString());

        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to undo delete");
        AppUserException.ThrowIfUnDoDeleteUnDeletedUser(userFoundById.IsDeleted);


        userFoundById.UnDoDelete();


        var result = await _userManager.UpdateAsync(userFoundById);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Undo delete user failed");


        return "Update user successfully";
    }



}
