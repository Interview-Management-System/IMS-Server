using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Features.UserFeature.UseCases;

public sealed class UserCreateUseCase : BaseUserUseCase
{

    public UserCreateUseCase(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(default!, default!, userManager, roleManager)
    {

    }


    internal async Task<string> CreateUserAsync(UserForCreateDTO userForCreateDTO)
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



    internal async Task<string> CreateCandidateAsync(CandidateForCreateDTO candidateForCreateDTO)
    {

        var userFound = await _userManager.FindByEmailAsync(candidateForCreateDTO.Email.Trim());
        AppUserException.ThrowIfUserExist(userFound);


        AppRole? role = await _roleManager.FindByIdAsync(candidateForCreateDTO.RoleId.ToString()!);
        ArgumentNullException.ThrowIfNull(role, "Role not found to add user");


        var candidate = _mapper.Map<Candidate>(candidateForCreateDTO);


        var createUserResult = await _userManager.CreateAsync(candidate, DEFAULT_PASSWORD);
        ApplicationException.ThrowIfOperationFail(createUserResult.Succeeded, "Create candidate failed");


        var addRoleResult = await _userManager.AddToRoleAsync(candidate, role.Name!.Trim());
        ApplicationException.ThrowIfOperationFail(addRoleResult.Succeeded, "Set role failed");


        return "Create candidate successfully";
    }
}
