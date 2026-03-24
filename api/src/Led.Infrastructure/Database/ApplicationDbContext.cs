using Microsoft.EntityFrameworkCore;

namespace Led.Infrastructure.Database;

internal sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);

#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging()
            .EnableDetailedErrors();
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Schemas.Default);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        //var modelForeignKeys = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());

        //foreach (var foreignKey in modelForeignKeys)
        //{
        //    foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        //}

        base.OnModelCreating(modelBuilder);
    }
}
