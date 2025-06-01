using InterviewManagementSystem.Application.Shared;
using InterviewManagementSystem.Application.Shared.Utilities;
using InterviewManagementSystem.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.RegularExpressions;
using ApplicationException = InterviewManagementSystem.Application.Shared.Exceptions.ApplicationException;

namespace InterviewManagementSystem.API.Exceptions;

internal sealed class ExceptionHandler : IExceptionHandler
{
    private static readonly string MESSAGE_PATTERN = @"'([^']*)'";

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        int statusCode;
        Match? match = null;

        switch (exception)
        {
            case ApplicationException:
                statusCode = StatusCodes.Status400BadRequest;
                break;


            case ArgumentNullException:
                statusCode = StatusCodes.Status404NotFound;
                match = Regex.Match(exception.Message, MESSAGE_PATTERN);
                break;


            case ImsError:
            case InvalidOperationException:
                statusCode = StatusCodes.Status500InternalServerError;
                match = Regex.Match(exception.Message, MESSAGE_PATTERN);
                break;


            default:
                statusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        httpContext.Response.StatusCode = statusCode;


        try
        {
            await httpContext.Response.WriteAsJsonAsync(ApiResponse.Create("", match?.Groups[1].Value), cancellationToken);
            IMSLogger.Error($"Request failed (code {statusCode}): {exception.Message}");
            return true;
        }
        catch (Exception e)
        {
            IMSLogger.Error($"Unknown error {e.Message}");
            return false;
        }
    }
}
