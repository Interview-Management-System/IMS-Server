using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Enums.Extensions;



namespace InterviewManagementSystem.Application.Features.UserFeature.UseCases;

public sealed class UserUpdateUseCase : BaseUserUseCase
{

    public UserUpdateUseCase(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(default!, default!, userManager, roleManager)
    {
    }



    internal async Task<string> UpdateUserAsync(UserForUpdateDTO userForUpdateDTO)
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










    internal async Task<string> UpdateCandidateAsync(CandidateForUpdateDTO candidateForUpdateDTO)
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




    internal async Task<string> ChangeUserRole(Guid userId, Guid roleId)
    {

        var userFoundById = await _userManager.FindByIdAsync(userId.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to assign new role");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        await UpdateUserRoleAsync(userFoundById, roleId);


        var result = await _userManager.UpdateAsync(userFoundById);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Change role failed");

        return "Change role successfully";
    }





    private async Task UpdateUserRoleAsync(AppUser appUser, Guid roleId)
    {

        // Get role
        var currentRoles = await _userManager.GetRolesAsync(appUser);
        ArgumentOutOfRangeException.ThrowIfEqual(currentRoles.Count, 0, "User has no roles");


        // Check role
        AppRole? role = await _roleManager.FindByNameAsync(currentRoles[0]);
        ArgumentNullException.ThrowIfNull(role, "Role not found to add user");


        // Assign new role
        if (role.Id != roleId)
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(appUser, currentRoles);
            ApplicationException.ThrowIfOperationFail(removeResult.Succeeded, "Remove old role failed");


            var changeRoleResult = await _userManager.AddToRoleAsync(appUser, roleId.GetRoleNameById());
            ApplicationException.ThrowIfOperationFail(changeRoleResult.Succeeded, "Change to new role failed");
        }
    }
}
