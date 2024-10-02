using System.Text;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace InterviewManagementSystem.API.Configurations;

internal static class AccessControl
{

    internal static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        string? secretKeyString = configuration["JWT:SecretKey"];
        ArgumentException.ThrowIfNullOrWhiteSpace(secretKeyString, "Secret key not found");


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




    internal static void AddRoleAuthorization(this IServiceCollection services)
    {

        string adminRole = RoleEnum.Admin.GetName();
        string managerRole = RoleEnum.Manager.GetName();
        string candidateRole = RoleEnum.Candidate.GetName();
        string recruiterRole = RoleEnum.Recruiter.GetName();
        string interviewRole = RoleEnum.Interviewer.GetName();


        string[] adminAndManagerRoles = [adminRole, managerRole];
        string[] adminManagerRecruiterRoles = [adminRole, managerRole, recruiterRole];
        string[] allRoles = [adminRole, managerRole, candidateRole, recruiterRole, interviewRole];
        string[] adminManagerRecruiterInterviewerRoles = [adminRole, managerRole, recruiterRole, interviewRole];


        services.AddAuthorizationBuilder()
            .AddPolicy(AuthorizationPolicy.RequiredAdmin, policy => policy.RequireRole(adminRole))
            .AddPolicy(AuthorizationPolicy.RequiredAllRoles, policy => policy.RequireRole(allRoles))
            .AddPolicy(AuthorizationPolicy.RequiredManager, policy => policy.RequireRole(managerRole))
            .AddPolicy(AuthorizationPolicy.RequiredInterviewerRole, policy => policy.RequireRole(interviewRole))
            .AddPolicy(AuthorizationPolicy.RequiredAdminManager, policy => policy.RequireRole(adminAndManagerRoles))
            .AddPolicy(AuthorizationPolicy.RequiredAdminManagerRecruiter, policy => policy.RequireRole(adminManagerRecruiterRoles))
            .AddPolicy(AuthorizationPolicy.RequiredAdminManagerRecruiterInterviewer, policy => policy.RequireRole(adminManagerRecruiterInterviewerRoles));
    }




    internal static void AddCrossOriginResourceSharing(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddDefaultPolicy(builder =>
                                                builder
                                                .AllowAnyOrigin()
                                                .AllowAnyMethod()
                                                .AllowAnyHeader())
        );
    }
}


internal static class AuthorizationPolicy
{
    public const string RequiredAdmin = "RequiredAdmin";
    public const string RequiredManager = "RequiredManager";
    public const string RequiredAllRoles = "RequiredAllRoles";
    public const string RequiredAdminManager = "RequiredAdminManager";
    public const string RequiredInterviewerRole = "RequiredInterviewerRole";
    public const string RequiredAdminManagerRecruiter = "RequiredAdminManagerRecruiter";
    public const string RequiredAdminManagerRecruiterInterviewer = "RequiredAdminManagerRecruiterInterviewer";

}