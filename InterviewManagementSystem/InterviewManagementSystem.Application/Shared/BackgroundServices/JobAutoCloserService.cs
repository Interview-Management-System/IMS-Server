using InterviewManagementSystem.Domain.Entities.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace InterviewManagementSystem.Application.Shared.BackgroundServices;

public sealed class JobAutoCloserService : BackgroundService
{

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<JobAutoCloserService> _logger;



    public JobAutoCloserService(IServiceProvider serviceProvider, ILogger<JobAutoCloserService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }



    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogError("Checking over due jobs");
                await CheckAndCloseOverdueJobsAsync();
                _logger.LogError("Done checking");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while closing overdue jobs");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }



    private async Task CheckAndCloseOverdueJobsAsync()
    {

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();



        Expression<Func<Job, bool>> expression = j =>
            j.JobStatusId == JobStatusEnum.Open
            && j.DatePeriod!.EndDate < DateTime.UtcNow;


        var overdueJobs = await dbContext
            .GetBaseRepository<Job>()
            .GetAllAsync(expression, true);


        foreach (var job in overdueJobs)
            job.CloseJob();


        await dbContext.SaveChangesAsync();
    }
}
