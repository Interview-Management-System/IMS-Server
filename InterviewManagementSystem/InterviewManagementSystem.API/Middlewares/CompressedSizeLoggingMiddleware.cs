using InterviewManagementSystem.Application.Shared.Utilities;

namespace InterviewManagementSystem.API.Middlewares
{
    public sealed class CompressedSizeLoggingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            await next(context);

            if (context.Response.Headers.TryGetValue("Content-Length", out var contentLength))
            {
                IMSLogger.Warn($"Compressed Response Size: {contentLength} bytes");
            }
            else
            {
                IMSLogger.Warn("Compressed Response Size: Not available (chunked transfer or compression removed Content-Length header)");
            }
        }
    }
}
