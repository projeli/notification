using MediatR;
using Projeli.NotificationService.Application.Dtos;
using Projeli.Shared.Domain.Models.Notifications;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Application.Commands;

public class CreateNotificationCommand : IRequest<IResult<NotificationDto>>
{
    public required NotificationDto Notification { get; init; }
}