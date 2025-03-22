using InterviewManagementSystem.Application.Services;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Infrastructure.Databases.Cloudinary;
using InterviewManagementSystem.Infrastructure.Databases.MongoDB;
using InterviewManagementSystem.Infrastructure.Databases.MongoDB.Repositories;
using InterviewManagementSystem.Infrastructure.Databases.PostgreSQL;
using InterviewManagementSystem.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace InterviewManagementSystem.Infrastructure.Extensions
{

    public static class InfrastructureExtension
    {

        public static void AddCloudinaryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySetting>(configuration.GetSection("IMS_CloudinarySetting"));
            services.AddSingleton<ICloudinaryService, CloudinaryService>();
        }



        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSetting>(configuration.GetSection("IMS_MongoDbSetting"));
            var mongoSettings = configuration.Get<MongoDbSetting>();

            string connectionString = configuration["IMS_MongoDbSetting:ConnectionString"] ?? "";
            string databaseName = configuration["IMS_MongoDbSetting:DatabaseName"] ?? "";

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            services.AddSingleton(database);
            //services.AddScoped(typeof(IMongoRepository<>), typeof(MongoBaseRepository<>));
            services.AddScoped<IMongoRepository<User>, UserRepo>();
        }



        public static void AddPostgreSqlDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var a = services.Configure<PostgresSetting>(configuration.GetSection("IMS_PostgreSqlSetting"));

            string connectionString = configuration["IMS_PostgreSqlSetting:ConnectionString"] ?? "";
            ArgumentNullException.ThrowIfNullOrEmpty(connectionString, "Connection string not found");

            // Setup DB
            services.AddDbContextPool<InterviewManagementSystemContext>(options => options.UseNpgsql(connectionString));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<InterviewManagementSystemContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
