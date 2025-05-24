using AutoMapper;
using MassTransit;
using MediatR;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Application.Dtos;
using Projeli.Shared.Application.Messages.Notifications;

namespace Projeli.NotificationService.Infrastructure.Messaging.Consumers;

public class AddNotificationsConsumer(IMediator mediator, IMapper mapper) : IConsumer<AddNotificationsMessage>
{
    public async Task Consume(ConsumeContext<AddNotificationsMessage> context)
    {
        var message = context.Message;
        var command = new CreateNotificationsCommand
            { Notifications = mapper.Map<List<NotificationDto>>(message.Notifications) };

        var result = await mediator.Send(command);

        if (!result.Success)
        {
            Console.WriteLine($"Failed to add notifications: {string.Join(", ", result.Errors.Values)}");
        }
    }
}