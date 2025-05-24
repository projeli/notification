using MediatR;
using Microsoft.EntityFrameworkCore;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Infrastructure.Commands;

public class MarkNotificationsCommandHandler(NotificationServiceWriteDbContext database)
    : IRequestHandler<MarkNotificationsCommand, IResult<bool>>
{
    public async Task<IResult<bool>> Handle(MarkNotificationsCommand request, CancellationToken cancellationToken)
    {
        var result = await database.Notifications
            .Where(x => request.UserId == x.UserId && x.IsRead != request.IsRead)
            .ExecuteUpdateAsync(
                x => x.SetProperty(
                    n => n.IsRead,
                    request.IsRead),
                cancellationToken: cancellationToken
            );

        return result > 0
            ? new Result<bool>(true)
            : new Result<bool>(false, request.IsRead
                    ? "Failed to mark all notifications as read."
                    : "Failed to mark all notifications as unread.",
                false
            );
    }
}