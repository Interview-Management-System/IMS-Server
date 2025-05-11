using InterviewManagementSystem.Application.Services;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Infrastructure.Databases.Cloudinary;
using InterviewManagementSystem.Infrastructure.Databases.PostgreSQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewManagementSystem.Infrastructure.Extensions
{

    public static class InfrastructureExtension
    {

        public static void AddCloudinaryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySetting>(configuration.GetSection("IMS_CloudinarySetting"));
            services.AddSingleton<ICloudinaryService, CloudinaryService>();
        }




        public static void AddPostgreSqlDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //var a = services.Configure<PostgresSetting>(configuration.GetSection("IMS_PostgreSqlSetting"));

            string connectionString = configuration["IMS_PostgreSqlSetting:ConnectionString"] ?? "";
            ArgumentNullException.ThrowIfNullOrEmpty(connectionString, "Connection string not found");

            // Setup DB
            services.AddDbContextPool<InterviewManagementSystemContext>(options =>
                    options.UseNpgsql(connectionString).EnableSensitiveDataLogging());

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<InterviewManagementSystemContext>()
                .AddDefaultTokenProviders();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
