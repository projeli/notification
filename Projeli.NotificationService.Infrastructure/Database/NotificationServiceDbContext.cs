using Microsoft.EntityFrameworkCore;
using Npgsql;
using Projeli.Shared.Domain.Models.Notifications;
using Projeli.Shared.Infrastructure.Converters;

namespace Projeli.NotificationService.Infrastructure.Database;

public class NotificationServiceDbContext<T>(DbContextOptions<T> options)
    : DbContext(options) where T : NotificationServiceDbContext<T>
{
    public DbSet<Notification> Notifications { get; set; } = null!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidToStringConverter>()
            .HaveConversion<UlidToGuidConverter>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(T).Assembly);
    }
}