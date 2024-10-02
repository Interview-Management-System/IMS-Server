using InterviewManagementSystem.Application.Exceptions;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace InterviewManagementSystem.Application.Features.AuthenticationFeatures.UseCases;

public sealed class PasswordUseCase : BaseAuthenticationUseCase
{


    public PasswordUseCase(UserManager<AppUser> userManager) : base(userManager)
    {
    }



    public async Task<string> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email);
        ArgumentNullException.ThrowIfNull(user, "User not found (Wrong email)");


        string newPassword = resetPasswordRequest.NewPassword;
        AppUserException.ThrowIfResetPasswordsNotEqual(newPassword, resetPasswordRequest.ConfirmPassword);


        var decodedToken = WebEncoders.Base64UrlDecode(resetPasswordRequest.Token);
        string normalToken = Encoding.UTF8.GetString(decodedToken);


        var resetResult = await _userManager.ResetPasswordAsync(user, normalToken, newPassword);
        AppUserException.ThrowIfResetPasswordFail(resetResult.Succeeded);


        return "Reset password successfully";
    }




    public Task<ApiResponse<string>> ForgetPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }
}
