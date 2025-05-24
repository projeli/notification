using System.Reflection;
using Projeli.NotificationService.Infrastructure.Commands;
using Projeli.NotificationService.Infrastructure.Queries;

namespace Projeli.NotificationService.Api.Extensions;

public static class MediatrExtension
{
    public static void AddNotificationServiceMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(GetUnreadNotificationsCountQueryHandler))!);
            cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(CreateNotificationCommandHandler))!);
        });
    }
}