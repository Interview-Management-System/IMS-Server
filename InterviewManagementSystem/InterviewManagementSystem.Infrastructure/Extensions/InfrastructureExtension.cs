using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Infrastructure.Databases.Cloudinary;
using InterviewManagementSystem.Infrastructure.Databases.PostgreSQL;
using InterviewManagementSystem.Infrastructure.Persistences;
using InterviewManagementSystem.Infrastructure.UnitOfWorks;
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
            services.AddSingleton<CloudinaryService>();
        }




        public static void AddPostgreSqlDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var a = services.Configure<PostgresSetting>(configuration.GetSection("IMS_PostgreSqlSetting"));

            var connectionString = configuration["IMS_PostgreSqlSetting:ConnectionString"];
            ArgumentNullException.ThrowIfNullOrEmpty(connectionString, "Connection string not found");

            // Setup DB
            services.AddDbContext<InterviewManagementSystemContext>(options => options.UseNpgsql(connectionString));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<InterviewManagementSystemContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
