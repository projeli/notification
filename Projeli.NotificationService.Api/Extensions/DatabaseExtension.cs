using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Infrastructure.Exceptions;

namespace Projeli.NotificationService.Api.Extensions;

public static class DatabaseExtension
{
    public static void AddNotificationServiceDatabase(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var readConnectionString = configuration["Database:ReadConnectionString"];
        var writeConnectionString = configuration["Database:WriteConnectionString"];

        if (string.IsNullOrEmpty(readConnectionString))
        {
            throw new MissingEnvironmentVariableException("Database:ReadConnectionString");
        }

        if (string.IsNullOrEmpty(writeConnectionString))
        {
            throw new MissingEnvironmentVariableException("Database:WriteConnectionString");
        }

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var readDataSourceBuilder = new NpgsqlDataSourceBuilder(readConnectionString);
        readDataSourceBuilder.EnableDynamicJson();
        readDataSourceBuilder.ConfigureJsonOptions(jsonSerializerOptions);
        var readDataSource = readDataSourceBuilder.Build();

        services.AddDbContext<NotificationServiceReadDbContext>(options =>
        {
            options.UseNpgsql(readDataSource,
                builder => { builder.MigrationsAssembly("Projeli.NotificationService.Api"); });

            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        });

        var writeDataSourceBuilder = new NpgsqlDataSourceBuilder(writeConnectionString);
        writeDataSourceBuilder.EnableDynamicJson();
        writeDataSourceBuilder.ConfigureJsonOptions(jsonSerializerOptions);
        var writeDataSource = writeDataSourceBuilder.Build();

        services.AddDbContext<NotificationServiceWriteDbContext>(options =>
        {
            options.UseNpgsql(writeDataSource,
                builder => { builder.MigrationsAssembly("Projeli.NotificationService.Api"); });

            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        });
    }

    public static void UseNotificationServiceDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var writeDatabase = scope.ServiceProvider.GetRequiredService<NotificationServiceWriteDbContext>();
        if (writeDatabase.Database.GetPendingMigrations().Any())
        {
            writeDatabase.Database.Migrate();
        }
    }
}