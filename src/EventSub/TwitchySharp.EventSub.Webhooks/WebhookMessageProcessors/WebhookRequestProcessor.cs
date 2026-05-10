using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Webhooks.Deserialization;
using TwitchySharp.EventSub.Webhooks.Requests;
using TwitchySharp.EventSub.Webhooks.Responses;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;

/// <summary>
/// A function processing EventSub webhook requests.
/// </summary>
/// <remarks>
/// Use the default output of <see cref="WebhookRequestProcessor.Create(IWebhookEventSubHandler, DeserializeWebhookRequest?)"/>
/// or define your own webhook request processing pipeline using this delegate.
/// </remarks>
/// <param name="header">The webhook request header.</param>
/// <param name="body">The webhook request body.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="ValueTask"/> containing the response for the request.</returns>
public delegate ValueTask<WebhookResponseData> ProcessWebhookRequest(EventSubWebhookRequestHeader header, NotificationPayloadStream body, CancellationToken ct);

/// <summary>
/// Contains methods for creating an EventSub webhook message processor.
/// </summary>
public static class WebhookRequestProcessor
{
    /// <summary>
    /// Create a webhook message processor.
    /// </summary>
    /// <param name="handler">The message side effect handler to use.</param>
    /// <param name="deserializeRequest">
    /// The request deserializer to use.
    /// Defaults to the output of <see cref="WebhookRequestDeserializer.Create(DeserializeNotification?, System.Text.Json.JsonSerializerOptions?)"/>.</param>
    /// <returns>A webhook message processor using the provided <paramref name="handler"/> and <paramref name="deserializeRequest"/>.</returns>
    public static ProcessWebhookRequest Create(
        IWebhookEventSubHandler handler,
        DeserializeWebhookRequest? deserializeRequest = null
        )
    {
        deserializeRequest ??= WebhookRequestDeserializer.Create();
        return (header, body, ct) => HandleRequest(handler, deserializeRequest, header, body, ct);
    }

    private static async ValueTask<WebhookResponseData> HandleRequest(
        IWebhookEventSubHandler handler,
        DeserializeWebhookRequest deserializeRequest,
        EventSubWebhookRequestHeader header,
        NotificationPayloadStream body,
        CancellationToken ct = default
        )
        => await (await deserializeRequest(header.TwitchEventsubMessageType, body, ct)).Match(
            onError: e => handler.Error(e, ct),
            onValid: data => data switch
            {
                NotificationRequestData notification => handler.Notification(notification, ct),
                CallbackVerificationRequestData callback => handler.CallbackVerification(callback.Subscription, callback.Challenge, ct),
                RevocationRequestData revocation => handler.Revocation(revocation.Subscription, ct),
                _ => throw new NotSupportedException("Unsupported webhook request type.")
            });

    private static async ValueTask<WebhookResponseData> Error(this IWebhookEventSubHandler handler, Error e, CancellationToken ct)
    {
        await handler.OnError(e, ct);
        return new InternalErrorResponseData();
    }

    private static async ValueTask<WebhookResponseData> CallbackVerification(this IWebhookEventSubHandler handler, EventSubSubscription newSubscription, string challenge, CancellationToken ct = default)
    {
        await handler.OnCallbackVerification(newSubscription, challenge, ct);
        return new CallbackVerificationResponseData { Challenge = challenge };
    }

    private static async ValueTask<WebhookResponseData> Notification(this IWebhookEventSubHandler handler, IEventSubNotification notification, CancellationToken ct = default)
    {
        await handler.OnNotified(notification, ct);
        return new NotificationResponseData();
    }

    private static async ValueTask<WebhookResponseData> Revocation(this IWebhookEventSubHandler handler, EventSubSubscription revokedSubscription, CancellationToken ct = default)
    {
        await handler.OnSubscriptionRevoked(revokedSubscription, ct);
        return new RevocationResponseData();
    }
}
