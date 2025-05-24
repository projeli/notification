using MediatR;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Application.Queries;

/// <summary>
/// Gets the 5 most recent notifications for a user and the count of unread notifications.
/// </summary>
public class GetUnreadNotificationsCountQuery : IRequest<IResult<int>>
{
    public required string UserId { get; init; }
}