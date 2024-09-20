using InterviewManagementSystem.API.Exceptions;

namespace InterviewManagementSystem.API.Configurations
{
    internal static class ExceptionHandlers
    {

        internal static void AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<ExceptionHandler>();
            services.AddProblemDetails();
        }
    }
}
