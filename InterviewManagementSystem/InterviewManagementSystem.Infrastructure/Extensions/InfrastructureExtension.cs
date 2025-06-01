using InterviewManagementSystem.Application.Services;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Infrastructure.Databases.Cloudinary;
using InterviewManagementSystem.Infrastructure.Databases.PostgreSQL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InterviewManagementSystem.Infrastructure.Extensions
{

    public static class InfrastructureExtension
    {

        public static void AddCloudinaryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySetting>(configuration.GetSection("IMS_CloudinarySetting"));
            services.AddSingleton<ICloudinaryService, CloudinaryService>();
        }




        public static void AddPostgreSqlDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {

            string connectionString = configuration["IMS_PostgreSqlSetting:ConnectionString"] ?? "";


            // If the environment is production, add the certificate path to the connection string
            if (env.IsProduction())
            {
                string apiProjectPath = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
                string certPath = $"{apiProjectPath}\\Properties\\IMS-Connection-Certificate.crt";
                connectionString += $"Root Certificate={certPath};";
            }


            // Setup DB
            services.AddDbContextPool<InterviewManagementSystemContext>(options => options.UseNpgsql(connectionString).EnableSensitiveDataLogging());

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<InterviewManagementSystemContext>()
                .AddDefaultTokenProviders();

            //services.AddNpgsqlDataSource(connectionString);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("System.Net.DontEnableIPv6Preference", false);
        }
    }
}
