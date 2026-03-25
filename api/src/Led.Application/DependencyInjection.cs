using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Led.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddLiteBus();

        return services;
    }

    private static IServiceCollection AddLiteBus(this IServiceCollection services)
    {
        services.AddLiteBus(builder =>
        {
            var assembly = typeof(DependencyInjection).Assembly;

            builder.AddCommandModule(action => action.RegisterFromAssembly(assembly));
        });

        return services;
    }
}
