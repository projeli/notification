using MediatR;
using Microsoft.EntityFrameworkCore;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Infrastructure.Commands;

public class DeleteNotificationCommandHandler(NotificationServiceWriteDbContext database)
    : IRequestHandler<DeleteNotificationCommand, IResult<bool>>
{
    public async Task<IResult<bool>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var result = await database.Notifications
            .Where(n => n.Id == request.NotificationId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0
            ? new Result<bool>(true)
            : Result<bool>.Fail("Failed to delete notification");
    }
}