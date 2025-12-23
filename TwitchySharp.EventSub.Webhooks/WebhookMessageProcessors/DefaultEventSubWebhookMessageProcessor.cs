using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.EventSub.NotificationConverters;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.CallbackVerifiers;
using TwitchySharp.EventSub.Webhooks.Requests;
using TwitchySharp.EventSub.Webhooks.Responses;
using TwitchySharp.Shared;

namespace TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;
public class DefaultEventSubWebhookMessageProcessor(
    IWebhookEventSubHandler handler,
    INotificationConverter? converter = null,
    IWebhookCallbackVerifier? callbackVerifier = null,
    JsonSerializerOptions? serializerOptions = null
    )
    : IEventSubWebhookMessageProcessor
{
    private readonly IWebhookEventSubHandler _handler = handler;
    private readonly INotificationConverter _converter = converter ?? new NotificationConverter();
    private readonly IWebhookCallbackVerifier _callbackVerifier = callbackVerifier ?? new DefaultWebhookCallbackVerifier();
    private readonly JsonSerializerOptions _serializerOptions = serializerOptions ?? JsonConfig.ApiOptions;

    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public async ValueTask<WebhookResponseData> HandleRequest(EventSubWebhookRequestHeader requestHeader, Stream bodyStream, CancellationToken ct = default)
        => requestHeader.TwitchEventsubMessageType switch
        {
            TwitchEventSubMessageTypes.NOTIFICATION => await JsonSerializer.DeserializeAsync<JsonElement>(bodyStream, _serializerOptions, ct).ConfigureAwait(false) switch
            {
                { ValueKind: JsonValueKind.Undefined or JsonValueKind.Null } => throw new NotSupportedException("Notification request body cannot be null or undefined literal."),
                { } json => await Notification(_converter.Deserialize(json), ct)
            }, 
            TwitchEventSubMessageTypes.WEBHOOK_CALLBACK_VERIFICATION => await JsonSerializer.DeserializeAsync<CallbackVerificationRequestData>(bodyStream, _serializerOptions, ct).ConfigureAwait(false) switch
            {
                { } data => await CallbackVerification(data.Subscription, data.Challenge, ct),
                _ => throw new NotSupportedException("Callback verification request body cannot be null or undefined literal.")
            },
            TwitchEventSubMessageTypes.REVOCATION => await JsonSerializer.DeserializeAsync<RevocationRequestData>(bodyStream, _serializerOptions, ct).ConfigureAwait(false) switch
            {
                { } data => await Revocation(data.Subscription, ct),
                _ => throw new NotSupportedException("Revocation request body cannot be null or undefined literal.")
            },
            _ => throw new NotSupportedException($"Unsupported EventSub message type: {requestHeader.TwitchEventsubMessageType}"),
        };

    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public async ValueTask<WebhookResponseData> HandleRequest(EventSubWebhookRequestHeader requestHeader, string body, CancellationToken ct = default)
        => requestHeader.TwitchEventsubMessageType switch
        {
            TwitchEventSubMessageTypes.NOTIFICATION => JsonSerializer.Deserialize<JsonElement>(body, _serializerOptions) switch
            {
                { ValueKind: JsonValueKind.Undefined or JsonValueKind.Null } => throw new NotSupportedException("Notification request body cannot be null or undefined literal."),
                { } json => await Notification(_converter.Deserialize(json), ct)
            },
            TwitchEventSubMessageTypes.WEBHOOK_CALLBACK_VERIFICATION => JsonSerializer.Deserialize<CallbackVerificationRequestData>(body, _serializerOptions) switch 
            {
                { } data => await CallbackVerification(data.Subscription, data.Challenge, ct),
                _ => throw new NotSupportedException("Callback verification request body cannot be null or undefined literal.")
            },
            TwitchEventSubMessageTypes.REVOCATION => JsonSerializer.Deserialize<RevocationRequestData>(body, _serializerOptions) switch
            {
                { } data => await Revocation(data.Subscription, ct),
                _ => throw new NotSupportedException("Revocation request body cannot be null or undefined literal.")
            },
            _ => throw new NotSupportedException($"Unsupported EventSub message type: {requestHeader.TwitchEventsubMessageType}"),
        };

    public async ValueTask<CallbackVerificationResponseData> CallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default)
    {
        await _handler.OnCallbackVerification(newSubscription, challenge, ct);
        return await _callbackVerifier.VerifyCallback(challenge, ct);
    }

    public async ValueTask<NotificationResponseData> Notification(IEventSubNotification notification, CancellationToken ct = default)
    {
        await _handler.OnNotified(notification, ct);
        return new NotificationResponseData();
    }

    public async ValueTask<RevocationResponseData> Revocation(EventSubSubscription revokedSubscription, CancellationToken ct = default)
    {
        await _handler.OnSubscriptionRevoked(revokedSubscription, ct);
        return new RevocationResponseData();
    }
}
