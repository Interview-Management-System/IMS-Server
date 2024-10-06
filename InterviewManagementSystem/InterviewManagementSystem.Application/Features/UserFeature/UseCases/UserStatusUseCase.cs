using InterviewManagementSystem.Application.CustomClasses.Exceptions;
using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Features.UserFeature.UseCases;

public sealed class UserStatusUseCase(UserManager<AppUser> userManager)
{


    private readonly UserManager<AppUser> _userManager = userManager;



    internal async Task<string> DeActivateUser(Guid id)
    {
        var userFoundById = await _userManager.FindByIdAsync(id.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to de-activate");
        AppUserException.ThrowIfUserNotActive(userFoundById);


        userFoundById.DeActivate();
        var result = await _userManager.UpdateAsync(userFoundById);


        ApplicationException.ThrowIfOperationFail(result.Succeeded, "De-activate user failed");
        return "De-activate user successfully";
    }




    internal async Task<string> ActivateUser(Guid id)
    {
        var userFoundById = await _userManager.FindByIdAsync(id.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to de-activate");
        AppUserException.ThrowIfUserNotActive(userFoundById);


        userFoundById.Activate();
        var result = await _userManager.UpdateAsync(userFoundById);


        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Activate user failed");
        return "Activate user successfully";
    }




    internal async Task<string> DeleteUserAsync(Guid id)
    {

        var userFoundById = await _userManager.FindByIdAsync(id.ToString());

        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to delete");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        userFoundById.Delete();


        var result = await _userManager.UpdateAsync(userFoundById);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Delete user failed");


        return "Update user successfully";
    }




    internal async Task<string> UnDoDeleteUserAsync(Guid id)
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
