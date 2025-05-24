using MediatR;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Application.Commands;

public class DeleteNotificationsCommand : IRequest<IResult<bool>>
{
    public required List<Ulid> NotificationIds { get; set; }
}