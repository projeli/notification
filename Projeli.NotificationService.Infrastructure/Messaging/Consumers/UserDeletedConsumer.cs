using MassTransit;
using MediatR;
using Projeli.NotificationService.Application.Commands;
using Projeli.Shared.Application.Messages.Users;

namespace Projeli.NotificationService.Infrastructure.Messaging.Consumers;

public class UserDeletedConsumer(IMediator mediator) : IConsumer<UserDeletedMessage>
{
    public async Task Consume(ConsumeContext<UserDeletedMessage> context)
    {
        var message = context.Message;
        var command = new DeleteUserNotificationsCommand { UserId = message.UserId };

        var result = await mediator.Send(command);

        if (!result.Success)
        {
            Console.WriteLine($"Failed to delete user: {string.Join(", ", result.Errors.Values)}");
        }
    }
}