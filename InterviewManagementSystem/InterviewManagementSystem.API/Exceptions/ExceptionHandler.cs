using System.Text.RegularExpressions;
using InterviewManagementSystem.Application.CustomClasses;
using InterviewManagementSystem.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace InterviewManagementSystem.API.Exceptions;

internal sealed class ExceptionHandler : IExceptionHandler
{
    private static readonly string MESSAGE_PATTERN = @"'([^']*)'";

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        var apiResponse = new ApiResponse<bool>()
        {
            Message = exception.Message
        };


        int statusCode;
        switch (exception)
        {
            case DomainException:
                statusCode = StatusCodes.Status400BadRequest;
                break;


            case ArgumentNullException:
                statusCode = StatusCodes.Status404NotFound;
                Match match = Regex.Match(exception.Message, MESSAGE_PATTERN);

                if (match.Success)
                    apiResponse.Message = match.Groups[1].Value;
                break;


            default:
                statusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        apiResponse.StatusCode = statusCode;
        httpContext.Response.StatusCode = statusCode;


        try
        {
            await httpContext.Response.WriteAsJsonAsync(apiResponse, cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
