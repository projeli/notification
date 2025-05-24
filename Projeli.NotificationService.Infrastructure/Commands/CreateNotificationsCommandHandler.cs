using AutoMapper;
using MediatR;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Application.Dtos;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Domain.Models.Notifications;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Infrastructure.Commands;

public class CreateNotificationsCommandHandler(NotificationServiceWriteDbContext database, IMapper mapper)
    : IRequestHandler<CreateNotificationsCommand, IResult<List<NotificationDto>>>
{
    public async Task<IResult<List<NotificationDto>>> Handle(CreateNotificationsCommand request,
        CancellationToken cancellationToken)
    {
        var notifications = mapper.Map<List<Notification>>(request.Notifications);

        foreach (var notification in notifications)
        {
            notification.Id = Ulid.NewUlid();
            notification.Timestamp = DateTime.UtcNow;
        }

        await database.Notifications.AddRangeAsync(notifications, cancellationToken);
        await database.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<List<NotificationDto>>(notifications);

        return new Result<List<NotificationDto>>(result);
    }
}