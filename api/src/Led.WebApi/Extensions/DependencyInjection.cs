using Led.WebApi.Middleware;
using Led.WebApi.Middlewares;

namespace Led.WebApi.Extensions;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMiddleware()
            .AddProblemDetails()
            .IncludeOpenApi();

        services.AddControllers();

        return services;
    }

    private static IServiceCollection AddMiddleware(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddScoped<RequestContextLoggingMiddleware>();

        return services;
    }

    private static IServiceCollection IncludeOpenApi(this IServiceCollection services)
    {
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        return services;
    }
}
