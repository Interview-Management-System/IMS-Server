using InterviewManagementSystem.Application.Features.AuthenticationFeatures.UseCases;

namespace InterviewManagementSystem.Application.Features.AuthenticationFeatures;

public sealed class AuthenticationFacade
{


    public LoginUseCase LoginUseCase { get; private set; }
    public PasswordUseCase PasswordUseCase { get; private set; }


    public AuthenticationFacade(LoginUseCase loginUseCase, PasswordUseCase passwordUseCase)
    {
        LoginUseCase = loginUseCase;
        PasswordUseCase = passwordUseCase;
    }


    public async Task<ApiResponse<object>> BasicLoginAsync(UserLoginRequest userLoginRequest)
    {
        return await LoginUseCase.LoginAsync(userLoginRequest);
    }


    public async Task<string> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
    {
        return await PasswordUseCase.ResetPasswordAsync(resetPasswordRequest);
    }
}
