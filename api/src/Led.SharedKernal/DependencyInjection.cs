using Led.SharedKernal.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace Led.SharedKernal;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedKernalServices(this IServiceCollection services)
    {
        services.AddSingleton<IAmbientUnitOfWork, AmbientUnitOfWork>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IUnitOfWorkManager, UnitOfWorkManager>();

        return services;
    }
}
