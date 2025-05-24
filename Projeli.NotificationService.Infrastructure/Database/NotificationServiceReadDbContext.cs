using Microsoft.EntityFrameworkCore;

namespace Projeli.NotificationService.Infrastructure.Database;

public class NotificationServiceReadDbContext(DbContextOptions<NotificationServiceReadDbContext> options)
    : NotificationServiceDbContext<NotificationServiceReadDbContext>(options)
{
}