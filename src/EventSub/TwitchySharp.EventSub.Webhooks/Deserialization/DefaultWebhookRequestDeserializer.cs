using System.Text.Json;
using TwitchySharp.EventSub.Interfaces;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.NotificationConverters;
using TwitchySharp.EventSub.Webhooks.Enums;
using TwitchySharp.EventSub.Webhooks.Requests;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Webhooks.Deserialization;

/// <summary>
/// The default <see cref="IWebhookRequestBodyDeserializer"/> for Twitch webhook requests.
/// </summary>
/// <param name="notificationConverter">
/// The notification converter to use when deserializing EventSub notification requests.
/// Defaults to <see cref="NotificationConverter"/> if left <see langword="null"/>.
/// </param>
/// <param name="jsonSerializerOptions">
/// The <see cref="JsonSerializerOptions"/> to use when deserializing webhook requests.
/// Defaults to <see cref="JsonConfig.ApiOptions"/> if left <see langword="null"/>.
/// </param>
public class DefaultWebhookRequestDeserializer(
    INotificationConverter? notificationConverter = null,
    JsonSerializerOptions? jsonSerializerOptions = null
    )
    : IWebhookRequestBodyDeserializer
{
    private readonly INotificationConverter _converter = notificationConverter ?? new NotificationConverter();
    private readonly JsonSerializerOptions _serializerOptions = jsonSerializerOptions ?? JsonConfig.ApiOptions;

    public async ValueTask<IWebhookRequestData> DeserializeRequestBody(EventSubWebhookMessageType messageType, Stream body, CancellationToken ct = default)
        => messageType.Value switch
        {
            // Some jank here because the request body itself can be a full IEventSubNotification type.
            // We have to wrap it within the NotificationRequestData to keep the IWebhookRequestData return type.
            EventSubWebhookMessageTypes.NOTIFICATION => _converter.Deserialize(await JsonSerializer.DeserializeAsync<JsonElement>(body, _serializerOptions, ct)) switch
            {
                IEventSubNotification notification => new NotificationRequestData() { Subscription = notification.Subscription, Notification = notification },
                _ => throw new InvalidOperationException("The notification conversion was invalid.")
            } as IWebhookRequestData,
            EventSubWebhookMessageTypes.WEBHOOK_CALLBACK_VERIFICATION => await JsonSerializer.DeserializeAsync<CallbackVerificationRequestData>(body, _serializerOptions, ct),
            EventSubWebhookMessageTypes.REVOCATION => await JsonSerializer.DeserializeAsync<RevocationRequestData>(body, _serializerOptions, ct),
            _ => throw new NotSupportedException($"Deserialization of message type {messageType} is not supported.")
        } ?? throw new NotSupportedException("Null literal values are not supported.");
}
