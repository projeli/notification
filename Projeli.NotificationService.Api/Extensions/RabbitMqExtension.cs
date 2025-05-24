using MassTransit;
using Projeli.NotificationService.Infrastructure.Messaging.Consumers;
using Projeli.Shared.Infrastructure.Exceptions;

namespace Projeli.NotificationService.Api.Extensions;

public static class RabbitMqExtension
{
    public static void UseNotificationServiceRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<AddNotificationConsumer>();
            x.AddConsumer<AddNotificationsConsumer>();
            x.AddConsumer<RemoveNotificationConsumer>();
            x.AddConsumer<UserDeletedConsumer>();
            
            x.UsingRabbitMq((context, config) =>
            {
                config.Host(configuration["RabbitMq:Host"] ?? throw new MissingEnvironmentVariableException("RabbitMq:Host"), "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"] ?? throw new MissingEnvironmentVariableException("RabbitMq:Username"));
                    h.Password(configuration["RabbitMq:Password"] ?? throw new MissingEnvironmentVariableException("RabbitMq:Password"));
                });
                
                config.ReceiveEndpoint("notification-add-notification-queue", e =>
                {
                    e.ConfigureConsumer<AddNotificationConsumer>(context);
                });
                
                config.ReceiveEndpoint("notification-add-notifications-queue", e =>
                {
                    e.ConfigureConsumer<AddNotificationsConsumer>(context);
                });
                
                config.ReceiveEndpoint("notification-remove-notification-queue", e =>
                {
                    e.ConfigureConsumer<RemoveNotificationConsumer>(context);
                });
                
                config.ReceiveEndpoint("notification-user-deleted-queue", e =>
                {
                    e.ConfigureConsumer<UserDeletedConsumer>(context);
                });
            });
        });
    }

    private static void PublishFanOut<T>(this IRabbitMqBusFactoryConfigurator configurator)
        where T : class
    {
        configurator.Publish<T>(y => y.ExchangeType = "fanout");
    }
}