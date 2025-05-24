using System.Reflection;
using Projeli.NotificationService.Api.Extensions;
using Projeli.NotificationService.Application.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNotificationServiceCors(builder.Configuration, builder.Environment);
builder.Services.AddNotificationServiceSwagger();
builder.Services.AddControllers().AddNotificationServiceJson();
builder.Services.AddNotificationServiceMediatr();
builder.Services.AddNotificationServiceDatabase(builder.Configuration, builder.Environment);
builder.Services.AddNotificationServiceAuthentication(builder.Configuration, builder.Environment);
builder.Services.UseNotificationServiceRabbitMq(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(NotificationProfile)));
builder.Services.AddNotificationServiceOpenTelemetry(builder.Logging, builder.Configuration);

var app = builder.Build();

app.UseNotificationServiceMiddleware();
app.MapControllers();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseNotificationServiceSwagger();
}

app.UseNotificationServiceCors();
app.UseNotificationServiceAuthentication();
app.UseNotificationServiceDatabase();
app.UseNotificationServiceOpenTelemetry();

app.Run();