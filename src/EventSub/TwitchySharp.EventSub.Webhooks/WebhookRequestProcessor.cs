using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Webhooks.Deserialization;
using TwitchySharp.Infrastructure.Functional;
using Microsoft.IO;
using TwitchySharp.EventSub.Webhooks.Http;

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
public delegate ValueTask<WebhookResponse> ProcessWebhookRequest(EventSubWebhookRequest request, CancellationToken ct);

/// <summary>
/// Contains methods for creating an EventSub webhook message processing pipeline.
/// </summary>
public static class WebhookRequestProcessor
{
    private static readonly RecyclableMemoryStreamManager _memoryManager = new();

    /// <summary>
    /// Create a webhook message processor.
    /// </summary>
    /// <param name="handler">The message side effect handler to use.</param>
    /// <param name="verifyHash">
    /// The request verifier to use. This ensures the requests were signed by Twitch using the secret sent when creating the subscription.
    /// Use <see cref="WebhookHashVerifier.Create(ResolveWebhookSecret)"/> with a <see cref="ResolveWebhookSecret"/> implementation (e.g. <see cref="SecretResolvers.CreateFixedSecretResolver(WebhookSecret)"/>).
    /// </param>
    /// <param name="deserializeRequest">
    /// The request deserializer to use.
    /// Defaults to the output of <see cref="WebhookRequestDeserializer.Create(DeserializeNotification?, System.Text.Json.JsonSerializerOptions?)"/>.</param>
    /// <returns>A webhook message processor using the provided <paramref name="handler"/> and <paramref name="deserializeRequest"/>.</returns>
    public static ProcessWebhookRequest Create(
        IWebhookEventSubHandler handler,
        VerifyWebhookHash? verifyHash = null,
        DeserializeWebhookRequest? deserializeRequest = null
        )
    {
        deserializeRequest ??= WebhookRequestDeserializer.Create();
        return (request, ct) =>
        {
            // We duplicate the request stream here because we need to:
            // 1. Read the stream to deserialize and obtain the subscription
            // 2. Use the subscription to resolve the secret and read the stream again to get the hash with the secret
            // So we tee the stream and use the RecyclableMemoryStream for the copy stream
            using RecyclableMemoryStream cryptoStream = _memoryManager.GetStream();
            using TeeStream teeStream = new(request.Content, cryptoStream);

            EventSubWebhookRequest toDeserialize = request with { Content = new(teeStream) };
            EventSubWebhookRequest toVerify = request with { Content = new(cryptoStream) };

            return deserializeRequest(toDeserialize, ct)
                .BindAsync((deserialized, ct) => verifyHash is null
                    ? ValueTask.FromResult(new Validation<WebhookRequestContent>(deserialized))
                    : verifyHash(deserialized.Subscription, toVerify, ct).MapAsync(_ => deserialized), ct)
                .NotifyHandler(handler, ct);
        };
    }

    private static ValueTask<WebhookResponse> NotifyHandler(
        this ValueTask<Validation<WebhookRequestContent>> message,
        IWebhookEventSubHandler handler,
        CancellationToken ct
        )
        => message.MatchAsync(
            onError: handler.Error,
            onValid: (data, ct) => data switch
            {
                NotificationRequestContent notification => handler.Notification(notification.Notification, ct),
                CallbackVerificationRequestContent callback => handler.CallbackVerification(callback.Subscription, callback.Challenge, ct),
                RevocationRequestContent revocation => handler.Revocation(revocation.Subscription, ct),
                _ => throw new NotSupportedException("Unsupported webhook request type.")
            }, ct);

    private static async ValueTask<WebhookResponse> Error(this IWebhookEventSubHandler handler, Error e, CancellationToken ct)
    {
        await handler.OnError(e, ct);
        return new InternalErrorResponse();
    }

    private static async ValueTask<WebhookResponse> CallbackVerification(this IWebhookEventSubHandler handler, EventSubSubscription newSubscription, string challenge, CancellationToken ct = default)
    {
        await handler.OnCallbackVerification(newSubscription, challenge, ct);
        return new CallbackVerificationResponse { Challenge = challenge };
    }

    private static async ValueTask<WebhookResponse> Notification(this IWebhookEventSubHandler handler, IEventSubNotification notification, CancellationToken ct = default)
    {
        await handler.OnNotified(notification, ct);
        return new NotificationResponse();
    }

    private static async ValueTask<WebhookResponse> Revocation(this IWebhookEventSubHandler handler, EventSubSubscription revokedSubscription, CancellationToken ct = default)
    {
        await handler.OnSubscriptionRevoked(revokedSubscription, ct);
        return new RevocationResponse();
    }
}
