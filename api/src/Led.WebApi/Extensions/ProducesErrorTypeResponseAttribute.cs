using System.Net.Mime;
using Led.SharedKernal.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Led.WebApi.Extensions;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ProducesErrorTypeResponseAttribute : Attribute, IApiResponseMetadataProvider
{
    public ProducesErrorTypeResponseAttribute(ErrorType errorType)
    {
        StatusCode = errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.AccessUnauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.AccessForbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => 0
        };

        Type = typeof(ProblemDetails);
    }

    public Type? Type { get; }

    public int StatusCode { get; }

    public void SetContentTypes(MediaTypeCollection contentTypes)
    {
        contentTypes.Add(MediaTypeNames.Application.Json);
    }
}
