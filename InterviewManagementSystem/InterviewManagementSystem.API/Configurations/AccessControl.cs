using InterviewManagementSystem.Application.Managers.AuthenticationManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InterviewManagementSystem.API.Configurations;


internal static class AccessControl
{

    internal static void AddIMSAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(opt =>
        {
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
       .AddJwtBearer(o =>
       {

           using var serviceProvider = services.BuildServiceProvider();
           var configuration = serviceProvider.GetRequiredService<IConfiguration>();
           var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

           ArgumentException.ThrowIfNullOrWhiteSpace(jwtSettings!.SecretKey, "Secret key not found");
           var secretKeyByte = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);


           o.SaveToken = true;
           o.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = false,
               ValidateAudience = false,
               ClockSkew = TimeSpan.Zero,
               //ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte)
           };

           // Use an event to log the ClockSkew value whenever a token is validated
           o.Events = new JwtBearerEvents
           {
               OnTokenValidated = context =>
               {
                   // Retrieve the configured ClockSkew
                   var clockSkew = context.Options.TokenValidationParameters.ClockSkew;

                   // Obtain a logger instance from the DI container
                   var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                   var logger = loggerFactory.CreateLogger("JwtBearer");

                   // Log the value - this will occur every time a token is successfully validated.
                   logger.LogInformation("JWT token validated with ClockSkew: {ClockSkew}", clockSkew);

                   return Task.CompletedTask;
               },

               OnAuthenticationFailed = context =>
               {
                   // Optionally log the ClockSkew value if token authentication fails.
                   var clockSkew = context.Options.TokenValidationParameters.ClockSkew;
                   var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                   var logger = loggerFactory.CreateLogger("JwtBearer");

                   logger.LogWarning("Authentication failed. ClockSkew used: {ClockSkew}. Error: {Error}", clockSkew, context.Exception.Message);

                   return Task.CompletedTask;
               }
           };
       });

        services.AddLogging(builder =>
        {
            builder.AddConsole()
                   .AddFilter("Microsoft.AspNetCore.Authentication", LogLevel.Debug)
                   .AddFilter("Microsoft.AspNetCore.Authorization", LogLevel.Debug);
        });
    }




    internal static void AddIMSAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();

        /*
        string adminRole = RoleEnum.Admin.GetDescription();
        string managerRole = RoleEnum.Manager.GetDescription();
        string candidateRole = RoleEnum.Candidate.GetDescription();
        string recruiterRole = RoleEnum.Recruiter.GetDescription();
        string interviewRole = RoleEnum.Interviewer.GetDescription();


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
            .AddPolicy(AuthorizationPolicy.RequiredAdminManagerRecruiterInterviewer, policy => policy.RequireRole(adminManagerRecruiterInterviewerRoles));*/
    }




    internal static void AddIMSCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddDefaultPolicy(builder => builder
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