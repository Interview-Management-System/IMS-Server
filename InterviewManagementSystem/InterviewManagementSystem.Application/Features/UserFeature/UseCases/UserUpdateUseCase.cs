using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;



namespace InterviewManagementSystem.Application.Features.UserFeature.UseCases;

public sealed class UserUpdateUseCase : BaseUserUseCase
{

    public UserUpdateUseCase(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(mapper, default!, userManager, roleManager)
    {
    }



    internal async Task<string> UpdateUserAsync(UserForUpdateDTO userForUpdateDTO)
    {

        var userFoundById = await _userManager.FindByIdAsync(userForUpdateDTO.Id.ToString());


        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to update");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        _mapper.Map(userForUpdateDTO, userFoundById);


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


        var result = await _userManager.UpdateAsync(candidate!);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Update candidate failed");


        return "Update candidate successfully";
    }


}
