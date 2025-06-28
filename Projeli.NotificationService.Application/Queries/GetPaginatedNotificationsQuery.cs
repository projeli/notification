using MediatR;
using Projeli.NotificationService.Application.Dtos;
using Projeli.Shared.Domain.Models.Notifications;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Application.Queries;

public class GetPaginatedNotificationsQuery : IRequest<PagedResult<NotificationDto>>
{
    public required string UserId { get; init; }
    public required NotificationType? NotificationType { get; init; }
    public required bool Unread { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
}