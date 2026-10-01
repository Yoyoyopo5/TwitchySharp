using System.Text.Json;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// A function processing EventSub webhook requests.
/// </summary>
/// <remarks>
/// Use <see cref="ProcessWebhookRequestSerializationExtensions.ByJsonDeserialization"/> to create a default pipeline.
/// </remarks>
/// <param name="request">The webhook request to process.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="ValueTask"/> containing a <see cref="Validation"/> containing the request.</returns>
public delegate ValueTask<Validation<IWebhookRequestContent>> ProcessWebhookRequest(EventSubWebhookRequest request, CancellationToken ct);

/// <summary>
/// Creation helpers for <see cref="ProcessWebhookRequest"/>.
/// </summary>
public static class ProcessWebhookRequestSerializationExtensions
{
    /// <summary>
    /// Occurs when a JSON payload is an unexpected <c>null</c> literal value.
    /// </summary>
    public record NullPayloadError() : Error("The request had a null literal JSON payload.");

    /// <summary>
    /// Occurs when the <c>Twitch-Event-Sub-Message-Type</c> request header is an unsupported value.
    /// </summary>
    /// <param name="MessageType">The message type that was received.</param>
    public record UnsupportedMessageTypeError(EventSubWebhookMessageType MessageType) : Error("The request had an unsupported payload type.");

    /// <summary>
    /// An exception occurred during request deserialization.
    /// </summary>
    /// <param name="JsonSerializerException">The exception.</param>
    public record DeserializationExceptionError(Exception JsonSerializerException)
        : Error(JsonSerializerException.Message);

    /// <summary>
    /// Contains options for configuring JSON deserialization of EventSub webhook requests.
    /// </summary>
    public record RequestDeserializationOptions
    {
        /// <summary>
        /// The function that should be used to polymorphically deserialize notification requests.
        /// </summary>
        /// <remarks>
        /// By default, uses <see cref="DeserializeNotificationExtensions.ByPolymorphicJsonDeserialization"/>.
        /// </remarks>
        public DeserializeNotification DeserializeNotification { get; init; }
            = DeserializeNotification.ByPolymorphicJsonDeserialization();
    }

    extension(ProcessWebhookRequest p)
    {
        /// <summary>
        /// Create a <see cref="ProcessWebhookRequest"/> pipeline by deserializing the incoming request content as JSON.
        /// </summary>
        /// <param name="configure">A function that takes a default set of options and returns a new set of configured options.</param>
        /// <returns>A <see cref="ProcessWebhookRequest"/> that uses JSON deserialization.</returns>
        public static ProcessWebhookRequest ByJsonDeserialization(
            Func<RequestDeserializationOptions, RequestDeserializationOptions>? configure = null
            )
        {
            RequestDeserializationOptions opts = configure is null ? new() : configure(new());
            Func<WebhookRequestContentStream, CancellationToken, ValueTask<Validation<NotificationRequestContent>>> deserializeNotification
                = opts.DeserializeNotification.ToWebhookNotificationDeserializer();

            return (request, ct) => request.Header.TwitchEventsubMessageType.Value switch
            {
                EventSubWebhookMessageTypes.WEBHOOK_CALLBACK_VERIFICATION => request.Content.Deserialize<CallbackVerificationRequestContent>(ct),
                EventSubWebhookMessageTypes.NOTIFICATION => deserializeNotification(request.Content, ct).MapAsync(v => v as IWebhookRequestContent),
                EventSubWebhookMessageTypes.REVOCATION => request.Content.Deserialize<RevocationRequestContent>(ct),
                _ => ValueTask.FromResult<Validation<IWebhookRequestContent>>(new UnsupportedMessageTypeError(request.Header.TwitchEventsubMessageType))
            };
        }

    }

    private async static ValueTask<Validation<IWebhookRequestContent>> Deserialize<TContent>(
        this WebhookRequestContentStream requestContent,
        CancellationToken ct
        )
        where TContent : IWebhookRequestContent
    {
        try
        {
            return await JsonSerializer.DeserializeAsync<TContent>(requestContent, JsonConfig.ApiOptions, ct) is not { } data
                ? new NullPayloadError()
                : data;
        }
        catch (Exception ex)
        {
            return new DeserializationExceptionError(ex);
        }
    }

    private static Func<WebhookRequestContentStream, CancellationToken, ValueTask<Validation<NotificationRequestContent>>> ToWebhookNotificationDeserializer(
        this DeserializeNotification deserialize)
        => (payload, ct) => deserialize(new(payload), ct).MapAsync(notification => new NotificationRequestContent() { Notification = notification });
}
