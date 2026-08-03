using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks;

public static class RequestHandlingExtensions
{
    public static ProcessWebhookRequest Map<T>(this ProcessWebhookRequest process, Func<T, CancellationToken, ValueTask> handleRequest)
        where T : WebhookRequestContent
        => async (request, ct) =>
        {
            Validation<WebhookRequestContent> result = await process(request, ct);
            return await result.Match<ValueTask<Validation<WebhookRequestContent>>>(
                onError: e => ValueTask.FromResult<Validation<WebhookRequestContent>>(e),
                onValid: async content =>
                {
                    if (content is T typedContent)
                        await handleRequest(typedContent, ct);
                    return content;
                });
        };

    public static ProcessWebhookRequest MapError(this ProcessWebhookRequest process, Func<Error, CancellationToken, ValueTask> handleError)
        => async (request, ct) =>
        {
            Validation<WebhookRequestContent> result = await process(request, ct);
            return await result.Match<ValueTask<Validation<WebhookRequestContent>>>(
                onError: async e =>
                {
                    await handleError(e, ct);
                    return e;
                },
                onValid: content => ValueTask.FromResult<Validation<WebhookRequestContent>>(content)
                );
        };

    public static ProcessWebhookRequest MapNotification<T>(this ProcessWebhookRequest process, Func<T, CancellationToken, ValueTask> handleNotification)
        where T : IEventSubNotification
        => process.Map<NotificationRequestContent>(
            (notificationContent, ct) => notificationContent.Notification is T notification
                ? handleNotification(notification, ct)
                : ValueTask.CompletedTask
            );

    public static ProcessWebhookRequest MapSubscriptionRevoked(this ProcessWebhookRequest process, Func<EventSubSubscription, CancellationToken, ValueTask> handleSubscriptionRevoked)
        => process.Map<RevocationRequestContent>((revocationContent, ct) => handleSubscriptionRevoked(revocationContent.Subscription, ct));

    public static ProcessWebhookRequest MapCallbackVerification(this ProcessWebhookRequest process, Func<EventSubSubscription, string, CancellationToken, ValueTask> handleCallbackVerification)
        => process.Map<CallbackVerificationRequestContent>((callbackVerificationContent, ct) => handleCallbackVerification(callbackVerificationContent.Subscription, callbackVerificationContent.Challenge, ct));
}
