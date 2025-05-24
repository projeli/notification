using AutoMapper;
using Projeli.NotificationService.Application.Dtos;
using Projeli.Shared.Application.Messages.Notifications;
using Projeli.Shared.Domain.Models.Notifications;

namespace Projeli.NotificationService.Application.Profiles;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<Notification, NotificationDto>();
        CreateMap<NotificationDto, Notification>();

        CreateMap<NotificationMessage, NotificationDto>();
    }
}