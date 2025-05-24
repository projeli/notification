using MediatR;
using Microsoft.EntityFrameworkCore;
using Projeli.NotificationService.Application.Queries;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Infrastructure.Queries;

/// <summary>
/// Gets the count of the users unread notifications.
/// </summary>
public class GetUnreadNotificationsCountQueryHandler(NotificationServiceReadDbContext database)
    : IRequestHandler<GetUnreadNotificationsCountQuery, IResult<int>>
{
    public async Task<IResult<int>> Handle(GetUnreadNotificationsCountQuery request, CancellationToken cancellationToken)
    {
        var unreadCount = await database.Notifications
            .AsNoTracking()
            .CountAsync(n => n.UserId == request.UserId && !n.IsRead, cancellationToken);
        
        return new Result<int>(unreadCount);
    }
}