using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Managers.UserManagers;

public abstract class BaseUserManager : BaseManager<AppUser>
{

    protected readonly UserManager<AppUser> _userManager;
    protected readonly RoleManager<AppRole> _roleManager;
    protected static readonly string DEFAULT_PASSWORD = "T@n75541972";


    protected BaseUserManager(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(mapper, unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }



    protected async Task UpdateUserRoleAsync(AppUser appUser, Guid roleId)
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
