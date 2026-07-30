using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks;

public static partial class ProcessWebhookRequestExtensions
{
    /// <summary>
    /// Register a handler to notify in the webhook processing pipeline.
    /// </summary>
    /// <param name="process">The processing pipeline.</param>
    /// <param name="handler">The handler to register.</param>
    /// <returns>A new processing pipeline composed of <paramref name="process"/> and <paramref name="handler"/>.</returns>
    public static ProcessWebhookRequest WithHandler(this ProcessWebhookRequest process, IWebhookEventSubHandler handler)
        => process.With(next => (request, ct) => process(request, ct).MatchAsync(
            onError: async (e, ct) =>
            {
                await handler.OnError(e, ct);
                return new Validation<WebhookRequestContent>(e);
            },
            onValid: async (content, ct) =>
            {
                switch (content)
                {
                    case NotificationRequestContent notification:
                        await handler.OnNotified(notification.Notification, ct);
                        break;
                    case CallbackVerificationRequestContent callback:
                        await handler.OnCallbackVerification(callback.Subscription, callback.Challenge, ct);
                        break;
                    case RevocationRequestContent revocation:
                        await handler.OnSubscriptionRevoked(revocation.Subscription, ct);
                        break;
                    default:
                        return new Error("Unsupported webhook request type.");
                }
                return new Validation<WebhookRequestContent>(content);
            },
            ct
            ));
}
