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

    public async ValueTask<WebhookResponseData> HandleRequest(EventSubWebhookRequestHeader requestHeader, Stream bodyStream, CancellationToken ct = default)
        => requestHeader.TwitchEventsubMessageType switch
        {
            TwitchEventSubMessageTypes.NOTIFICATION => await Notification(_converter.Deserialize(JsonSerializer.Deserialize<JsonElement>(await JsonSerializer.DeserializeAsync<JsonElement>(bodyStream, _serializerOptions, ct).ConfigureAwait(false), _serializerOptions)), ct),
            TwitchEventSubMessageTypes.WEBHOOK_CALLBACK_VERIFICATION => await CallbackVerification((await JsonSerializer.DeserializeAsync<CallbackVerificationRequestData>(bodyStream, _serializerOptions, ct).ConfigureAwait(false))?.Challenge, ct),
            TwitchEventSubMessageTypes.REVOCATION => await Revocation((await JsonSerializer.DeserializeAsync<RevocationRequestData>(bodyStream, _serializerOptions, ct).ConfigureAwait(false))?.Subscription, ct),
            _ => throw new NotSupportedException($"Unsupported EventSub message type: {requestHeader.TwitchEventsubMessageType}"),
        };

    public async ValueTask<WebhookResponseData> HandleRequest(EventSubWebhookRequestHeader requestHeader, string body, CancellationToken ct = default)
        => requestHeader.TwitchEventsubMessageType switch
        {
            TwitchEventSubMessageTypes.NOTIFICATION => await Notification(_converter.Deserialize(JsonSerializer.Deserialize<JsonElement>(body, _serializerOptions)), ct),
            TwitchEventSubMessageTypes.WEBHOOK_CALLBACK_VERIFICATION => await CallbackVerification(JsonSerializer.Deserialize<CallbackVerificationRequestData>(body, _serializerOptions)?.Challenge, ct),
            TwitchEventSubMessageTypes.REVOCATION => await Revocation(JsonSerializer.Deserialize<RevocationRequestData>(body, _serializerOptions)?.Subscription, ct),
            _ => throw new NotSupportedException($"Unsupported EventSub message type: {requestHeader.TwitchEventsubMessageType}"),
        };

    public ValueTask<CallbackVerificationResponseData> CallbackVerification(string? challenge, CancellationToken ct = default)
        => challenge switch
        {
            { } => _callbackVerifier.VerifyCallback(challenge, ct),
            _ => ValueTask.FromResult(new CallbackVerificationResponseData() { StatusCode = 400, Challenge = string.Empty })
        };

    public ValueTask<NotificationResponseData> Notification(IEventSubNotification? notification, CancellationToken ct = default)
        => notification switch
        {
            { } => _handler.OnNotified(notification, ct),
            _ => ValueTask.FromResult(new NotificationResponseData() { StatusCode = 400 })
        };

    public ValueTask<RevocationResponseData> Revocation(EventSubSubscription? revokedSubscription, CancellationToken ct = default)
        => revokedSubscription switch
        {
            { } => _handler.OnSubscriptionRevoked(revokedSubscription, ct),
            _ => ValueTask.FromResult(new RevocationResponseData() { StatusCode = 400 })
        };
}
