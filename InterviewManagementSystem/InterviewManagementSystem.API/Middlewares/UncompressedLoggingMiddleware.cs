using InterviewManagementSystem.Application.Shared.Utilities;
using System.Text;

namespace InterviewManagementSystem.API.Middlewares
{
    public sealed class UncompressedLoggingMiddleware(RequestDelegate next)
    {

        private readonly RequestDelegate _next = next;



        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;


            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;


            await _next(context);


            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var uncompressedBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var uncompressedSize = Encoding.UTF8.GetByteCount(uncompressedBody);


            IMSLogger.Warn($"Uncompressed Response Size: {uncompressedSize} bytes");


            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
