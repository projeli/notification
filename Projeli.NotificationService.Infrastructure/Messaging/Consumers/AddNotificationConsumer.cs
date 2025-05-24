using AutoMapper;
using MassTransit;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Application.Dtos;
using Projeli.Shared.Application.Messages.Notifications;
using IMediator = MediatR.IMediator;

namespace Projeli.NotificationService.Infrastructure.Messaging.Consumers;

public class AddNotificationConsumer(IMediator mediator, IMapper mapper) : IConsumer<AddNotificationMessage>
{
    public async Task Consume(ConsumeContext<AddNotificationMessage> context)
    {
        var message = context.Message;
        var command = new CreateNotificationCommand { Notification = mapper.Map<NotificationDto>(message) };

        var result = await mediator.Send(command);
        
        if (!result.Success)
        {
            Console.WriteLine($"Failed to create notification: {string.Join(", ", result.Errors.Values)}");
        }
    }
}