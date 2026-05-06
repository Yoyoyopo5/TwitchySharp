using System.Text.Json;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Webhooks.CallbackVerifiers;
using TwitchySharp.EventSub.Webhooks.Deserialization;
using TwitchySharp.EventSub.Webhooks.Requests;
using TwitchySharp.EventSub.Webhooks.Responses;

namespace TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;
/// <summary>
/// The default <see cref="IEventSubWebhookMessageProcessor"/>.
/// Handles deserialization of requests and generates responses. 
/// </summary>
/// <param name="handler">
/// The notification handler to use.
/// Supply your own custom <see cref="IWebhookEventSubHandler"/> to define what
/// you want to happen when notifications are received.
/// </param>
/// <param name="callbackVerifier">
/// The callback verifier to use.
/// Defaults to <see cref="DefaultWebhookCallbackVerifier"/> if left <see langword="null"/>.
/// </param>
/// <param name="requestDeserializer">
/// The request deserializer to use.
/// Defaults to <see cref="DefaultWebhookRequestDeserializer"/> if left <see langword="null"/>.
/// </param>
public class DefaultEventSubWebhookMessageProcessor(
    IWebhookEventSubHandler handler,
    IWebhookCallbackVerifier? callbackVerifier = null,
    IWebhookRequestBodyDeserializer? requestDeserializer = null
    )
    : IEventSubWebhookMessageProcessor
{
    private readonly IWebhookEventSubHandler _handler = handler;
    private readonly IWebhookRequestBodyDeserializer _deserializer = requestDeserializer ?? new DefaultWebhookRequestDeserializer();
    private readonly IWebhookCallbackVerifier _callbackVerifier = callbackVerifier ?? new DefaultWebhookCallbackVerifier();

    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public async ValueTask<WebhookResponseData> HandleRequest(EventSubWebhookRequestHeader requestHeader, Stream bodyStream, CancellationToken ct = default)
        => await _deserializer.DeserializeRequestBody(requestHeader.TwitchEventsubMessageType, bodyStream, ct) switch
        {
            NotificationRequestData notification => await Notification(notification.Notification, ct),
            CallbackVerificationRequestData callback => await CallbackVerification(callback.Subscription, callback.Challenge, ct),
            RevocationRequestData revocation => await Revocation(revocation.Subscription, ct),
            _ => throw new NotSupportedException($"Request body was not a supported type.")
        };

    private async ValueTask<CallbackVerificationResponseData> CallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default)
    {
        await _handler.OnCallbackVerification(newSubscription, challenge, ct);
        return await _callbackVerifier.VerifyCallback(challenge, ct);
    }

    private async ValueTask<NotificationResponseData> Notification(IEventSubNotification notification, CancellationToken ct = default)
    {
        await _handler.OnNotified(notification, ct);
        return new NotificationResponseData();
    }

    private async ValueTask<RevocationResponseData> Revocation(EventSubSubscription revokedSubscription, CancellationToken ct = default)
    {
        await _handler.OnSubscriptionRevoked(revokedSubscription, ct);
        return new RevocationResponseData();
    }
}
