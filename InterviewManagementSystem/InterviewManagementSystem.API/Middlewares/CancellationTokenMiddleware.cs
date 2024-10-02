using InterviewManagementSystem.Application.CustomClasses;

namespace InterviewManagementSystem.API.Middlewares;

internal sealed class CancellationTokenMiddleware(RequestDelegate next)
{

    private readonly RequestDelegate _next = next;


    public async Task InvokeAsync(HttpContext context)
    {
        CancellationTokenProvider.CancellationToken = context.RequestAborted;
        await _next(context);
    }



}
