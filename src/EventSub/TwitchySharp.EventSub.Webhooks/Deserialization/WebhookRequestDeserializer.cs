using System.Text.Json;
using TwitchySharp.EventSub.Webhooks.Enums;
using TwitchySharp.EventSub.Webhooks.Requests;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Webhooks.Deserialization;

/// <summary>
/// Deserializes EventSub webhook bodies.
/// </summary>
/// <param name="request">The webhook request.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>
/// A <see cref="ValueTask"/> containing a <see cref="Validation{T}"/> of <see cref="IWebhookRequestData"/>.
/// The validation can contain a <see cref="WebhookRequestDeserializer.DeserializationError"/>.
/// The <see cref="IWebhookRequestData"/> can be one of
/// <see cref="CallbackVerificationRequestData"/>,
/// <see cref="NotificationRequestData"/>,
/// or <see cref="RevocationRequestData"/>.
/// </returns>
public delegate ValueTask<Validation<IWebhookRequestData>> DeserializeWebhookRequest(EventSubWebhookRequest request, CancellationToken ct);

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
    public static DeserializeWebhookRequest Create(
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

    private static DeserializeWebhookRequest CreateDeserializer(
        Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<IWebhookRequestData>>> callback,
        Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<IWebhookRequestData>>> notification,
        Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<IWebhookRequestData>>> revocation
        )
        => (request, ct) => request.Header.TwitchEventsubMessageType.Value switch
        {
            EventSubWebhookMessageTypes.WEBHOOK_CALLBACK_VERIFICATION => callback(request.Content, ct),
            EventSubWebhookMessageTypes.NOTIFICATION => notification(request.Content, ct),
            EventSubWebhookMessageTypes.REVOCATION => revocation(request.Content, ct),
            _ => throw new InvalidOperationException()
        };

    private static Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<IWebhookRequestData>>> CreateCallbackVerificationDeserializer(JsonSerializerOptions options)
        => async (payload, ct) => await JsonSerializer.DeserializeAsync<CallbackVerificationRequestData>(payload, options, ct) is not { } data
            ? new DeserializationError("Callback verification request had a null payload.")
            : data;

    private static Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<IWebhookRequestData>>> CreateRevocationDeserializer(JsonSerializerOptions options)
        => async (payload, ct) => await JsonSerializer.DeserializeAsync<RevocationRequestData>(payload, options, ct) is not { } data
            ? new DeserializationError("Revocation webhook request had a null payload.")
            : data;

    private static Func<NotificationPayloadStream, CancellationToken, ValueTask<Validation<IWebhookRequestData>>> CreateNotificationDeserializer(DeserializeNotification deserialize)
        => async (payload, ct) => (await deserialize(payload, ct)).Match<Validation<IWebhookRequestData>>(
            onError: e => new DeserializationError("An error occurred during notification deserialization. See the inner error for details.") { InnerError = e },
            onValid: notification => new NotificationRequestData() { Subscription = notification.Subscription, Notification = notification }
            );
}
