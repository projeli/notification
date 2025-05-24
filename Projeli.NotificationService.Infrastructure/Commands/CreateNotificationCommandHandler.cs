using AutoMapper;
using MediatR;
using Projeli.NotificationService.Application.Commands;
using Projeli.NotificationService.Application.Dtos;
using Projeli.NotificationService.Infrastructure.Database;
using Projeli.Shared.Domain.Models.Notifications;
using Projeli.Shared.Domain.Results;

namespace Projeli.NotificationService.Infrastructure.Commands;

public class CreateNotificationCommandHandler(NotificationServiceWriteDbContext database, IMapper mapper)
    : IRequestHandler<CreateNotificationCommand, IResult<NotificationDto>>
{

    public async Task<IResult<NotificationDto>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = mapper.Map<Notification>(request.Notification);

        notification.Id = Ulid.NewUlid();
        notification.Timestamp = DateTime.UtcNow;

        await database.Notifications.AddAsync(notification, cancellationToken);
        await database.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<NotificationDto>(notification);

        return new Result<NotificationDto>(result);
    }
}