using FluentResults;
using Led.SharedKernal.Errors;
using Led.SharedKernal.FluentResult;
using Microsoft.AspNetCore.Mvc;

namespace Led.WebApi.Extensions;

// TODO: Consider making this a middleware instead for logging
internal static class ResultExtensions
{
    private static readonly Dictionary<ErrorType, short> _errorTypePriorityOrder = new()
    {
        {ErrorType.Failure, 1 },
        {ErrorType.AccessForbidden, 2 },
        {ErrorType.AccessUnauthorized, 3 },
        {ErrorType.NotFound, 4 },
        {ErrorType.Conflict, 5 },
        {ErrorType.Validation, 6 }
    };

    public static IActionResult MatchResult(this Result result, Func<IActionResult> onSuccess)
    {
        return result.IsSuccess ? onSuccess() : MapFailure(result.Errors);
    }

    public static IActionResult EvaluateResult<TIn>(this Result<TIn> result, Func<object?, IActionResult> onSuccess)
    {
        return result.IsSuccess ? onSuccess(result.Value) : MapFailure(result.Errors);
    }

    private static ObjectResult MapFailure(IReadOnlyList<IError> errors)
    {
        var errorGroups = errors.GroupBy(e => e.GetErrorType(), e => e)
            .OrderBy(eg => _errorTypePriorityOrder.GetValueOrDefault(eg.Key, short.MaxValue))
            .ToArray();

        var errorType = errorGroups[0];

        var problemDetails = errorType.Key switch
        {
            ErrorType.NotFound => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Type = "NotFound",
                Title = "Not found",
                Detail = "One or more requested recources cannot be found"
            },
            ErrorType.Validation => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation failure",
                Detail = "One or more validation errors occurred"
            },
            ErrorType.Conflict => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Type = "Conflict",
                Title = "Conflict",
                Detail = "One or more conflicts occurred"
            },
            ErrorType.AccessUnauthorized => new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Type = "Authorization",
                Title = "Unauthorized",
                Detail = "Unauthorized to access requested resource"
            },
            ErrorType.AccessForbidden => new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Type = "Forbidden",
                Title = "Forbidden",
                Detail = "Forbidden"
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "ServerError",
                Title = "Server error",
                Detail = "An unexpected error occurred"
            }
        };

        if (problemDetails.Status < 500)
        {
            problemDetails.Extensions["errors"] = errorType.Select(e => new
            {
                ErrorCode = e.GetErrorCode(),
                ErrorMessage = e.Message
            });
        }

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }
}
