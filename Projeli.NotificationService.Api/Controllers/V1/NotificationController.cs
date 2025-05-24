using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Application.Queries;
using Projeli.NotificationService.Application.Requests;
using Projeli.Shared.Domain.Models.Notifications;
using Projeli.Shared.Infrastructure.Extensions;

namespace Projeli.NotificationService.Api.Controllers.V1;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("v1/notifications")]
public class NotificationController(IMediator mediator) : BaseController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPaginatedNotifications(
        [FromQuery] NotificationType? type = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        pageSize = Math.Clamp(pageSize, 1, 100);
        
        var query = new GetPaginatedNotificationsQuery
        {
            UserId = User.GetId(),
            NotificationType = type,
            Page = page,
            PageSize = pageSize
        };
        var result = await mediator.Send(query);
        
        return HandleResult(result);
    }

    [HttpGet("unread")]
    [Authorize]
    public async Task<IActionResult> GetUnreadNotificationsCount()
    {
        var query = new GetUnreadNotificationsCountQuery { UserId = User.GetId() };
        var result = await mediator.Send(query);

        return HandleResult(result);
    }
    
    [HttpPost("read")]
    [Authorize]
    public async Task<IActionResult> MarkAllNotificationsAsRead()
    {
        var command = new MarkNotificationsCommand
        {
            UserId = User.GetId(),
            IsRead = true
        };
        var result = await mediator.Send(command);

        return HandleResult(result);
    }
    
    [HttpPut("{id}/mark")]
    [Authorize]
    public async Task<IActionResult> MarkNotificationAsRead([FromRoute] Ulid id, [FromBody] MarkNotificationRequest request)
    {
        var command = new MarkNotificationCommand
        {
            UserId = User.GetId(),
            NotificationId = id,
            IsRead = request.IsRead
        };
        var result = await mediator.Send(command);

        return HandleResult(result);
    }
}