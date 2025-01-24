using InterviewManagementSystem.Application.Shared;
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

        var apiResponse = new ApiResponse<bool>()
        {
            Message = exception.Message
        };


        int statusCode;
        Match? match = null;

        switch (exception)
        {
            case DomainException:
            case ApplicationException:
                statusCode = StatusCodes.Status400BadRequest;
                break;


            case ArgumentNullException:
                statusCode = StatusCodes.Status404NotFound;
                match = Regex.Match(exception.Message, MESSAGE_PATTERN);
                break;


            case InvalidOperationException:
                statusCode = StatusCodes.Status500InternalServerError;
                match = Regex.Match(exception.Message, MESSAGE_PATTERN);
                break;


            default:
                statusCode = StatusCodes.Status500InternalServerError;
                break;
        }


        if (match?.Success == true)
        {
            apiResponse.Message = match.Groups[1].Value;
        }

        //apiResponse.StatusCode = statusCode;
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
