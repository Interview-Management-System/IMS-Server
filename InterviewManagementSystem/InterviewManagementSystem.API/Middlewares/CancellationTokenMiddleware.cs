using InterviewManagementSystem.Application.CustomClasses;

namespace InterviewManagementSystem.API.Middlewares;

internal sealed class CancellationTokenMiddleware(RequestDelegate next)
{

    private readonly RequestDelegate _next = next;


    public async Task InvokeAsync(HttpContext context)
    {
        CancellationTokenProvider.CancellationToken = context.RequestAborted;

        try
        {
            await _next(context);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"Request canceled for: {context.Request.Path}");
        }
    }
}
