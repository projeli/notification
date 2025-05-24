using Projeli.Shared.Domain.Models.Notifications;

namespace Projeli.NotificationService.Application.Dtos;

public class NotificationDto
{
    public required Ulid Id { get; set; }
    public required string UserId { get; set; }
    public required NotificationType Type { get; set; }
    public required NotificationBody Body { get; set; }
    public required bool IsRead { get; set; }
    public required DateTime Timestamp { get; set; }
}