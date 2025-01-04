using InterviewManagementSystem.Domain.Entities.AppUsers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterviewManagementSystem.Application.Features.AuthenticationFeatures.UseCases;

public sealed class LoginUseCase : BaseAuthenticationUseCase
{


    private readonly IConfiguration _configuration;



    public LoginUseCase(UserManager<AppUser> userManager, IConfiguration configuration) : base(userManager)
    {
        _configuration = configuration;
    }



    internal async Task<ApiResponse<UserLoginResponse>> LoginAsync(UserLoginRequest userLoginRequest)
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
                UserName = user.UserName,
                Token = await GenerateJwtToken(user),
            },
            Message = "Login successfully"
        };
    }





    private async Task<string> GenerateJwtToken(AppUser user)
    {
        var userRole = (await _userManager.GetRolesAsync(user)).First();


        var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Role, userRole),
                new(ClaimTypes.Email, user.Email!)
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
