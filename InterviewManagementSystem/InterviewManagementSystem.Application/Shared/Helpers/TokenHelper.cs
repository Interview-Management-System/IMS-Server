using InterviewManagementSystem.Application.Managers.AuthenticationManager;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InterviewManagementSystem.Application.Shared.Helpers
{
    public sealed class TokenHelper
    {

        private static JwtSettings _jwtSettings = null!;


        public TokenHelper(IOptions<JwtSettings> option)
        {
            _jwtSettings = option.Value;
        }


        public static string GenerateJwtToken(AppUser user, string userRole)
        {

            var claims = new List<Claim>
            {
                new (ClaimTypes.Role, userRole),
                new ("picture", user.AvatarLink!),
                new (JwtRegisteredClaimNames.Email, user.Email!),
                new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new (JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.Expiration),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );


            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }


        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }



        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false // We want to validate the token even if it's expired
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken a);

            ArgumentNullException.ThrowIfNull(principal, "Token is not valid");
            return principal;
        }
    }
}
