using Microsoft.EntityFrameworkCore;

namespace Projeli.NotificationService.Infrastructure.Database;

public class NotificationServiceWriteDbContext(DbContextOptions<NotificationServiceWriteDbContext> options)
    : NotificationServiceDbContext<NotificationServiceWriteDbContext>(options)
{
}