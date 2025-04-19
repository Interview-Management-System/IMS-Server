using InterviewManagementSystem.Domain.Entities.AppUsers;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace InterviewManagementSystem.Application.Managers.AuthenticationManager;


public class JwtSettings
{
    public int Expiration { get; set; }
    public string? SecretKey { get; set; }
    public int RefreshTokenExpiration { get; set; }
}



public sealed class AuthenticationManager
{

    private readonly UserManager<AppUser> _userManager;



    public AuthenticationManager(UserManager<AppUser> userManager, TokenHelper tokenHelper)
    {
        _userManager = userManager;
        //_tokenHelper = tokenHelper;


    }


    private void A()
    {
        _userManager.RemoveAuthenticationTokenAsync(null, "Default", "Default").Wait();
        _userManager.GenerateUserTokenAsync(null, "Default", "Default").Wait();
    }


    public async Task<ApiResponse<UserLoginResponse>> BasicLoginAsync(UserLoginRequest userLoginRequest)
    {
        AppUser? user = await _userManager.FindByEmailAsync(userLoginRequest.Email);


        ArgumentNullException.ThrowIfNull(user, "User not found (Wrong email)");
        AppUserException.ThrowIfUserNotActive(user);


        bool isValidPassword = await _userManager.CheckPasswordAsync(user, userLoginRequest.Password);
        AppUserException.ThrowIfWrongPassword(isValidPassword);

        // update user to add refresh token
        // add 2 cols for user are resfresh token and refresh token expiration
        // _userManager.UpdateAsync(user).Wait();

        var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        ArgumentNullException.ThrowIfNullOrEmpty(userRole, "User role not found");


        return new ApiResponse<UserLoginResponse>
        {
            Data = new UserLoginResponse
            {
                Token = TokenHelper.GenerateJwtToken(user, userRole),
                RefreshToken = TokenHelper.GenerateRefreshToken()
            },
            Message = "Login successfully"
        };
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
