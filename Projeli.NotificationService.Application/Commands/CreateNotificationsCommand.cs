using MediatR;
using Projeli.NotificationService.Application.Dtos;
using Projeli.Shared.Domain.Models.Notifications;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Application.Commands;

public class CreateNotificationsCommand : IRequest<IResult<List<NotificationDto>>>
{
    public required List<NotificationDto> Notifications { get; init; }
}