using MediatR;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Application.Commands;

public class MarkNotificationCommand : IRequest<IResult<bool>>
{
    public required string UserId { get; set; }
    public required Ulid NotificationId { get; set; }
    public required bool IsRead { get; set; }
}