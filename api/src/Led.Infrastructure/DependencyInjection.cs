using Led.Domain.Tenants.Repositories;
using Led.Infrastructure.Clock;
using Led.Infrastructure.Database;
using Led.Infrastructure.Database.Abstraction;
using Led.Infrastructure.Repositories;
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
            .AddClock();
        //services.AddLiteBus();

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

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddClock(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
