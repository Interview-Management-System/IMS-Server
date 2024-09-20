using InterviewManagementSystem.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Exceptions;

internal sealed class ExceptionHandler : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {


        var problemDetails = new ProblemDetails()
        {
            Detail = exception.Message,
        };


        if (exception is DomainException)
        {
            problemDetails.Title = "Not found";
            problemDetails.Status = StatusCodes.Status404NotFound;
            httpContext.Response.StatusCode = 404;
        }
        else
        {
            problemDetails.Title = "An error occurred";
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            httpContext.Response.StatusCode = 500;
        }

        try
        {
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
