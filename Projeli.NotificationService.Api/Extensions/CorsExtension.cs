﻿namespace Projeli.NotificationService.Api.Extensions;

public static class CorsExtension
{
    public static void AddNotificationServiceCors(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                corsBuilder =>
                {
                    if (environment.IsProduction())
                    {
                        // configure for deployments
                        corsBuilder
                            .WithOrigins($"https://{configuration["ClientUrl"]}");
                    }
                    else
                    {
                        corsBuilder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                    }

                    corsBuilder
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
    
    public static void UseNotificationServiceCors(this IApplicationBuilder app)
    {
        app.UseCors();
    }
}