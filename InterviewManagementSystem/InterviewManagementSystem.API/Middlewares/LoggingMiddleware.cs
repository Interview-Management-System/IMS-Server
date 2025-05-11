using InterviewManagementSystem.Application.Shared.Utilities;
using System.Diagnostics;

namespace InterviewManagementSystem.API.Middlewares
{
    public sealed class LoggingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            await next(context);
            stopwatch.Stop();

            string path = context.Request.Path;
            string method = context.Request.Method;
            int statusCode = context.Response.StatusCode;

            IMSLogger.Success($"Request success (code {statusCode}): Method {method} of route {path} in {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
