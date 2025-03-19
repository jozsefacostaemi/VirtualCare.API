using System.Text.Json;
using Notifications;

namespace Auth.VirtualCare.API.Middlewares.Interceptors;
public class SignalRAfterResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SignalRAfterResponseMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    // Lista de endpoints en los que queremos ejecutar SignalR
    private readonly HashSet<string> _signalREndpoints = new()
    {
        "/Auth/Login",
        "/AuthomatedAuth/AuthomatedLogin",
        "/AuthomatedAuth/AuthomatedLogOut"
    };

    public async Task Invoke(HttpContext context)
    {
        var originalResponseBody = context.Response.Body;

        try
        {
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await _next(context);

                memoryStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
                memoryStream.Seek(0, SeekOrigin.Begin);

                await memoryStream.CopyToAsync(originalResponseBody);
                await originalResponseBody.FlushAsync();

                if (context.Request.Path.Value is not null && _signalREndpoints.Contains(context.Request.Path.Value))
                {
                    object? payloadData = null;

                    if (!string.IsNullOrWhiteSpace(responseBody))
                    {
                        try
                        {
                            using (JsonDocument doc = JsonDocument.Parse(responseBody))
                            {
                                if (doc.RootElement.TryGetProperty("data", out JsonElement dataElement))
                                {
                                    payloadData = JsonSerializer.Deserialize<object>(dataElement.GetRawText());
                                }
                            }
                        }
                        catch (JsonException)
                        {
                            // Handle JSON parsing error if necessary
                        }
                    }

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var notificationRepository = scope.ServiceProvider.GetRequiredService<NotificationRepository>();
                        await notificationRepository.SendBroadcastAsync(Notifications.Enums.NotificationEventCodeEnum.AttentionMessage, payloadData);
                    }
                }
            }
        }
        finally
        {
            context.Response.Body = originalResponseBody;
        }
    }
}
