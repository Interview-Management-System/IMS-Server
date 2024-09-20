using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace InterviewManagementSystem.API.Configurations;

internal static class AccessControl
{

    internal static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        string? secretKeyString = configuration["JWT:SecretKey"];
        ArgumentException.ThrowIfNullOrWhiteSpace(secretKeyString);



        var secretKeyByte = Encoding.UTF8.GetBytes(secretKeyString);

        services.AddAuthentication(opt =>
        {
            //opt.DefaultScheme = "Windows";
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddCookie(x => x.Cookie.Name = "token")
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
                ClockSkew = TimeSpan.Zero,
            };
            o.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["token"];
                    return Task.CompletedTask;
                }
            };
            o.SaveToken = true;
        });
    }




    internal static void AddAuthorization(this IServiceCollection services)
    {

    }




    internal static void AddCrossOriginResourceSharing(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddDefaultPolicy(
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            )
        );
    }
}
