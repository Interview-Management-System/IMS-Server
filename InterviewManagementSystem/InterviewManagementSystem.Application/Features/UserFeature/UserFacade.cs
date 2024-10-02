using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Application.Features.UserFeature.UseCases;

namespace InterviewManagementSystem.Application.Features.UserFeature;

public sealed class UserFacade
{

    internal UserCreateUseCase UserCreateUseCase { get; private set; }
    internal UserStatusUseCase UserStatusUseCase { get; private set; }
    internal UserUpdateUseCase UserUpdateUseCase { get; private set; }
    internal UserRetrieveUseCase UserRetrieveUseCase { get; private set; }
    internal CandidateStatusUseCase CandidateStatusUseCase { get; private set; }



    public UserFacade(UserCreateUseCase userCreateUseCase, UserRetrieveUseCase userRetrieveUseCase, UserStatusUseCase userStatusUseCase, UserUpdateUseCase userUpdateUseCase, CandidateStatusUseCase candidateStatusUseCase)
    {
        UserCreateUseCase = userCreateUseCase;
        UserStatusUseCase = userStatusUseCase;
        UserUpdateUseCase = userUpdateUseCase;
        UserRetrieveUseCase = userRetrieveUseCase;
        CandidateStatusUseCase = candidateStatusUseCase;
    }




    public async Task<string> CreateUserAsync(UserForCreateDTO userForCreateDTO)
    {
        return await UserCreateUseCase.CreateUserAsync(userForCreateDTO);
    }


    public async Task<ApiResponse<List<UserForRetrieveDTO>>> GetListUserAsync()
    {
        return await UserRetrieveUseCase.GetListAsync();
    }



    public async Task<ApiResponse<UserForRetrieveDTO>> GetUserByIdAsync(Guid id)
    {
        return await UserRetrieveUseCase.GetUserByIdAsync(id);
    }



    public async Task<string> SetCandidateStatus(Guid id, CandidateStatusEnum candidateStatusEnum)
    {
        return await CandidateStatusUseCase.SetCandidateStatus(id, candidateStatusEnum);
    }



    public async Task<ApiResponse<CandidateForRetrieveDTO>> GetCandidateByIdAsync(Guid id)
    {
        return await UserRetrieveUseCase.GetCandidateByIdAsync(id);
    }



    public async Task<string> CreateCandidateAsync(CandidateForCreateDTO candidateForCreateDTO)
    {
        return await UserCreateUseCase.CreateCandidateAsync(candidateForCreateDTO);
    }


    public async Task<string> UpdateCandidateAsync(CandidateForUpdateDTO candidateForUpdateDTO)
    {
        return await UserUpdateUseCase.UpdateCandidateAsync(candidateForUpdateDTO);
    }



    public async Task<ApiResponse<CandidateForRetrieveDTO>> UpdateCandidateAsync()
    {
        return null;
    }



    public async Task<string> DeActivateUserAsync(Guid id)
    {
        return await UserStatusUseCase.DeActivateUser(id);
    }


    public async Task<string> ActivateUserAsync(Guid id)
    {
        return await UserStatusUseCase.ActivateUser(id);
    }


    public async Task<string> DeleteUserAsync(Guid id)
    {
        return await UserStatusUseCase.DeleteUserAsync(id);
    }


    public async Task<string> UndoDeleteUserAsync(Guid id)
    {
        return await UserStatusUseCase.UnDoDeleteUserAsync(id);
    }


    public async Task<string> UpdateUserAsync(UserForUpdateDTO userForUpdateDTO)
    {
        return await UserUpdateUseCase.UpdateUserAsync(userForUpdateDTO);
    }
}
