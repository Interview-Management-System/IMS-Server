namespace InterviewManagementSystem.Application.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<string>> ForgetPasswordAsync(string email);
    }
}
