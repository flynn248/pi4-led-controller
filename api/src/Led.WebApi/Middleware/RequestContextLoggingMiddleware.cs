using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace Led.WebApi.Middleware;

internal sealed class RequestContextLoggingMiddleware : IMiddleware
{
    private const string CorrelationIdHeader = "X-Correlation-ID";

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
        {
            return next.Invoke(context);
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(CorrelationIdHeader, out StringValues correlationId);

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}
