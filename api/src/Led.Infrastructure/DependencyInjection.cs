using Led.Application.Abstraction.SSH;
using Led.Domain.Abstraction;
using Led.Infrastructure.Clock;
using Led.Infrastructure.Database;
using Led.Infrastructure.Database.Abstraction;
using Led.Infrastructure.SSH;
using Led.SharedKernal.Clock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Led.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase()
            .AddRepositories()
            .AddClock()
            .AddSsh();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(filter => filter.AssignableTo(typeof(IRepository<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddTransient(typeof(IDbContextProvider<>), typeof(DbContextProvider<>));

        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql("Host=led.database;Port=5432;Database=ledApp;Username=postgres;Password=postgres;Include Error Detail=true", options =>
            {
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default);
            });
        });

        return services;
    }

    private static IServiceCollection AddClock(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddSsh(this IServiceCollection services)
    {
        services.AddTransient<ISshService, SshService>();

        return services;
    }
}
