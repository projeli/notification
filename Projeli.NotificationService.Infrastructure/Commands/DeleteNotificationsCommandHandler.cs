using MediatR;
using Microsoft.EntityFrameworkCore;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Infrastructure.Commands;

public class DeleteNotificationsCommandHandler(NotificationServiceWriteDbContext database)
    : IRequestHandler<DeleteNotificationsCommand, IResult<bool>>
{
    public async Task<IResult<bool>> Handle(DeleteNotificationsCommand request, CancellationToken cancellationToken)
    {
        var result = await database.Notifications
            .Where(n => request.NotificationIds.Contains(n.Id))
            .ExecuteDeleteAsync(cancellationToken);

        return result >= request.NotificationIds.Count
            ? new Result<bool>(true)
            : Result<bool>.Fail("Failed to delete notifications");
    }
}