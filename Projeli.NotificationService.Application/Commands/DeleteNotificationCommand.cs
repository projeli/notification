using MediatR;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Application.Commands;

public class DeleteNotificationCommand : IRequest<IResult<bool>>
{
    public required Ulid NotificationId { get; set; }
}