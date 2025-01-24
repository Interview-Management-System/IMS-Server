using InterviewManagementSystem.Domain.Entities.AppUsers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterviewManagementSystem.Application.Managers.AuthenticationManager;

public sealed class AuthenticationManager
{

    private readonly IConfiguration _configuration;

    private readonly UserManager<AppUser> _userManager;



    public AuthenticationManager(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }



    public async Task<ApiResponse<UserLoginResponse>> BasicLoginAsync(UserLoginRequest userLoginRequest)
    {
        AppUser? user = await _userManager.FindByEmailAsync(userLoginRequest.Email);


        ArgumentNullException.ThrowIfNull(user, "User not found (Wrong email)");
        AppUserException.ThrowIfUserNotActive(user);


        bool isValidPassword = await _userManager.CheckPasswordAsync(user, userLoginRequest.Password);
        AppUserException.ThrowIfWrongPassword(isValidPassword);


        return new ApiResponse<UserLoginResponse>
        {
            Data = new UserLoginResponse
            {
                Token = await GenerateJwtToken(user),
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









    private async Task<string> GenerateJwtToken(AppUser user)
    {
        var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();


        var claims = new List<Claim>
            {
                new (ClaimTypes.Role, userRole!),
                new (ClaimTypes.Email, user.Email!),
                new (ClaimTypes.Name, user.UserName!),
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:Expiration"])),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );


        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return jwtToken;
    }
}
