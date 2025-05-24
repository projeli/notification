using MediatR;
using Microsoft.EntityFrameworkCore;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Infrastructure.Commands;

public class DeleteUserNotificationsCommandHandler(NotificationServiceWriteDbContext database) : IRequestHandler<DeleteUserNotificationsCommand, IResult<bool>>
{
    public async Task<IResult<bool>> Handle(DeleteUserNotificationsCommand request, CancellationToken cancellationToken)
    {
        var count = await database.Notifications
            .CountAsync(n => n.UserId == request.UserId, cancellationToken);
        
        var result = await database.Notifications
            .Where(n => n.UserId == request.UserId)
            .ExecuteDeleteAsync(cancellationToken);
        
        return result >= count
            ? new Result<bool>(true)
            : Result<bool>.Fail("Failed to delete user notifications");
    }
}