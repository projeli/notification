using System.Text.Json;
using System.Text.Json.Serialization;
using Projeli.Shared.Application.Converters;

namespace Projeli.NotificationService.Api.Extensions;

public static class JsonExtension
{
    public static void AddNotificationServiceJson(this IMvcBuilder services)
    {
        services.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
    }
}