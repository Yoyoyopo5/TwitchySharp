using System.Text.Json;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Webhooks.Serialization;

/// <summary>
/// Provides converter factories for EventSub webhook messages.
/// </summary>
public static class WebhookRequestDeserializer
{
    /// <summary>
    /// Error occuring during webhook request deserialization.
    /// </summary>
    /// <param name="Message">The error message.</param>
    public sealed record DeserializationError(string Message) : Error(Message);

    /// <summary>
    /// Creates a converter that can be used to deserialize incoming EventSub request messages.
    /// </summary>
    /// <param name="deserializeNotification">
    /// The notification deserializer to use.
    /// Defaults to the default output of <see cref="NotificationDeserializer.CreateDeserializer(Func{EventSubSubscriptionType, Func{JsonSerializerOptions, JsonDocument, Models.Notifications.IEventSubNotification}}?, JsonSerializerOptions?)"/>
    /// </param>
    /// <param name="serializerOptions">
    /// The serializer options to use. Defaults to <see cref="JsonConfig.ApiOptions"/>.
    /// </param>
    /// <returns>A function that deserializes individual webhook requests using the given <paramref name="deserializeNotification"/> and <paramref name="serializerOptions"/>.</returns>
    public static ProcessWebhookRequest Create(
        DeserializeNotification? deserializeNotification = null,
        JsonSerializerOptions? serializerOptions = null
        )
    {
        deserializeNotification ??= NotificationDeserializer.CreateDeserializer();
        serializerOptions ??= JsonConfig.ApiOptions;

        return CreateDeserializer(
            callback: CreateCallbackVerificationDeserializer(serializerOptions),
            notification: CreateNotificationDeserializer(deserializeNotification),
            revocation: CreateRevocationDeserializer(serializerOptions)
            );
    }

    private static ProcessWebhookRequest CreateDeserializer(
        Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<WebhookRequestContent>>> callback,
        Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<WebhookRequestContent>>> notification,
        Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<WebhookRequestContent>>> revocation
        )
        => (request, ct) => request.Header.TwitchEventsubMessageType.Value switch
        {
            EventSubWebhookMessageTypes.WEBHOOK_CALLBACK_VERIFICATION => callback(request.Content, ct),
            EventSubWebhookMessageTypes.NOTIFICATION => notification(request.Content, ct),
            EventSubWebhookMessageTypes.REVOCATION => revocation(request.Content, ct),
            _ => ValueTask.FromResult<Validation<WebhookRequestContent>>(new DeserializationError("Unsupported webhook message type."))
        };

    private static Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<WebhookRequestContent>>> CreateCallbackVerificationDeserializer(JsonSerializerOptions options)
        => async (payload, ct) => await JsonSerializer.DeserializeAsync<CallbackVerificationRequestContent>(payload, options, ct) is not { } data
            ? new DeserializationError("Callback verification request had a null payload.")
            : data;

    private static Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<WebhookRequestContent>>> CreateRevocationDeserializer(JsonSerializerOptions options)
        => async (payload, ct) => await JsonSerializer.DeserializeAsync<RevocationRequestContent>(payload, options, ct) is not { } data
            ? new DeserializationError("Revocation webhook request had a null payload.")
            : data;

    private static Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<WebhookRequestContent>>> CreateNotificationDeserializer(DeserializeNotification deserialize)
        => async (payload, ct) => (await deserialize(payload, ct)).Match<Validation<WebhookRequestContent>>(
            onError: e => new DeserializationError("An error occurred during notification deserialization. See the inner error for details.") { InnerError = e },
            onValid: notification => new NotificationRequestContent() { Subscription = notification.Subscription, Notification = notification }
            );
}
