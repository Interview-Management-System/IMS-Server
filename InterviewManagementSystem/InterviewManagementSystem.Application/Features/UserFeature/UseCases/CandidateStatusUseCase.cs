using InterviewManagementSystem.Domain.Entities.AppUsers;
using Microsoft.AspNetCore.Identity;

namespace InterviewManagementSystem.Application.Features.UserFeature.UseCases;

public sealed class CandidateStatusUseCase(UserManager<AppUser> userManager)
{


    private readonly UserManager<AppUser> _userManager = userManager;




    public async Task<string> SetCandidateStatus(Guid id, CandidateStatusEnum candidateStatusEnum)
    {
        var candidate = await GetCandidateByIdAsync(id);
        CandidateException.ThrowIfSetTheSameStatus(candidate!, candidateStatusEnum);

        candidate.SetCandidateStatus(candidateStatusEnum);

        var result = await _userManager.UpdateAsync(candidate);
        ApplicationException.ThrowIfOperationFail(result.Succeeded, "Activate user failed");

        return "Change candidate status successfully";
    }




    private async Task<Candidate> GetCandidateByIdAsync(Guid id)
    {
        var userFoundById = await _userManager.FindByIdAsync(id.ToString());
        ArgumentNullException.ThrowIfNull(userFoundById, "User not found to de-activate");


        if (userFoundById is not Candidate)
            throw new InvalidOperationException("Not type of candidate");


        return (userFoundById as Candidate)!;
    }



}
