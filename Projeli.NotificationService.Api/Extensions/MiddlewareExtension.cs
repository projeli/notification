using Projeli.NotificationService.Api.Middlewares;

namespace Projeli.NotificationService.Api.Extensions;

public static class MiddlewareExtension
{
    public static void UseNotificationServiceMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<DatabaseExceptionMiddleware>();
        builder.UseMiddleware<HttpExceptionMiddleware>();
    }
}