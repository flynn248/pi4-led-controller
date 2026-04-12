using System.Reflection;
using FluentValidation;
using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Led.Application;

public static class DependencyInjection
{
    private static readonly Assembly _assembly = typeof(DependencyInjection).Assembly;

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddLiteBus();

        // Add FluentValidation validators
        services.AddValidatorsFromAssembly(_assembly, includeInternalTypes: true);

        return services;
    }

    private static IServiceCollection AddLiteBus(this IServiceCollection services)
    {
        services.AddLiteBus(builder =>
        {
            builder.AddCommandModule(action =>
            {
                //action.Register(typeof(ValidationBehavior<>));
                action.RegisterFromAssembly(_assembly);
            });
        });

        return services;
    }
}
