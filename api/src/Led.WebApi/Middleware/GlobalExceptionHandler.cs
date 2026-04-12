using Led.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Led.WebApi.Middlewares;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionDetails = GetExceptionDetails(exception);

        var problemDetails = new ProblemDetails
        {
            Status = exceptionDetails.Status,
            Type = exceptionDetails.Type,
            Title = exceptionDetails.Title,
            Detail = exceptionDetails.Detail
        };

        if (exceptionDetails.Errors is not null)
        {
            problemDetails.Extensions["errors"] = exceptionDetails.Errors;
        }

        if (exceptionDetails.Status >= 500)
        {
            logger.LogError(exception, "Unhandled exception occurred");
        }

        httpContext.Response.StatusCode = exceptionDetails.Status;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException ex => new ExceptionDetails(StatusCodes.Status400BadRequest,
                                                           "ValidationFailure",
                                                           "Validation failure",
                                                           "One ore more validation failures have occurred",
                                                           ex.Errors),

            _ => new ExceptionDetails(StatusCodes.Status500InternalServerError,
                                      "ServerError",
                                      "Server error",
                                      "An unexpected error has occurred",
                                      null)
        };
    }

    private sealed record ExceptionDetails(int Status, string Type, string Title, string Detail, IEnumerable<object>? Errors);
}
