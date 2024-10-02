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



}
