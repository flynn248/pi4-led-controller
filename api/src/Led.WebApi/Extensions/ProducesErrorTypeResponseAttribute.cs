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
        StatusCode = errorType.GetHttpStatusCode();
        Type = typeof(ProblemDetails);
    }

    public Type? Type { get; }

    public int StatusCode { get; }

    public void SetContentTypes(MediaTypeCollection contentTypes)
    {
        contentTypes.Add(MediaTypeNames.Application.Json);
    }
}
