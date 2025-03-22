using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL
{
    public class InterviewManagementSystemContextFactory : IDesignTimeDbContextFactory<InterviewManagementSystemContext>
    {
        public InterviewManagementSystemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InterviewManagementSystemContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=sa");

            return new InterviewManagementSystemContext(optionsBuilder.Options);
        }
    }
}
