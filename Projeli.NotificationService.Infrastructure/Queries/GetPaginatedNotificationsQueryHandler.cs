using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Projeli.NotificationService.Application.Dtos;
using Projeli.NotificationService.Application.Queries;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Infrastructure.Queries;

public class GetPaginatedNotificationsQueryHandler(NotificationServiceReadDbContext database, IMapper mapper)
    : IRequestHandler<GetPaginatedNotificationsQuery, PagedResult<NotificationDto>>
{
    public async Task<PagedResult<NotificationDto>> Handle(GetPaginatedNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = database.Notifications
            .Where(n => n.UserId == request.UserId &&
                        (request.NotificationType == null || n.Type == request.NotificationType))
            .OrderByDescending(n => n.Timestamp);

        var totalCount = await notifications.CountAsync(cancellationToken);

        var paginatedNotifications = await notifications
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var result = mapper.Map<List<NotificationDto>>(paginatedNotifications);

        return new PagedResult<NotificationDto>(
            data: result,
            page: request.Page,
            pageSize: request.PageSize,
            totalCount: totalCount,
            totalPages: (int)Math.Ceiling((double)totalCount / request.PageSize)
        );
    }
}