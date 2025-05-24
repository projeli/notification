using MediatR;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Application.Commands;

public class MarkNotificationsCommand : IRequest<IResult<bool>>
{
    public required string UserId { get; set; }
    public required bool IsRead { get; set; }
}