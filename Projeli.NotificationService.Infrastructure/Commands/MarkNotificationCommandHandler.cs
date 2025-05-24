using MediatR;
using Microsoft.EntityFrameworkCore;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Infrastructure.Commands;

public class MarkNotificationCommandHandler(NotificationServiceWriteDbContext database)
    : IRequestHandler<MarkNotificationCommand, IResult<bool>>
{
    public async Task<IResult<bool>> Handle(MarkNotificationCommand request, CancellationToken cancellationToken)
    {
        var result = await database.Notifications
            .Where(x => x.Id == request.NotificationId && x.UserId == request.UserId && x.IsRead != request.IsRead)
            .ExecuteUpdateAsync(x => x.SetProperty(
                    n => n.IsRead,
                    request.IsRead),
                cancellationToken
            );

        return result > 0
            ? new Result<bool>(true)
            : new Result<bool>(false,
                request.IsRead
                    ? "Failed to mark notification as read."
                    : "Failed to mark notification as unread.",
                false
            );
    }
}