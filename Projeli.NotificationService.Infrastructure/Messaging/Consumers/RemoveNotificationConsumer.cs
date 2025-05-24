using MassTransit;
using MediatR;
using Projeli.NotificationService.Application.Commands;
using Projeli.Shared.Application.Messages.Notifications;

namespace Projeli.NotificationService.Infrastructure.Messaging.Consumers;

public class RemoveNotificationConsumer(IMediator mediator) : IConsumer<RemoveNotificationMessage>
{
    public async Task Consume(ConsumeContext<RemoveNotificationMessage> context)
    {
        var message = context.Message;
        var command = new DeleteNotificationCommand { NotificationId = message.NotificationId };

        var result = await mediator.Send(command);

        if (!result.Success)
        {
            Console.WriteLine($"Failed to delete notification: {string.Join(", ", result.Errors.Values)}");
        }
    }
}