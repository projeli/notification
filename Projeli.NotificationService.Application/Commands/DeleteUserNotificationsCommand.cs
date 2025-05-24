using MediatR;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Application.Commands;

public class DeleteUserNotificationsCommand : IRequest<IResult<bool>>
{
    public required string UserId { get; set; }
}