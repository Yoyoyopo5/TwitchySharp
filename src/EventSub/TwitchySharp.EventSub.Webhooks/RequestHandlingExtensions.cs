using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks;

public static class RequestHandlingExtensions
{
    /// <summary>
    /// Assign a function that is called when an <see cref="EventSubWebhookRequest"/> with content type of <typeparamref name="T"/> is received.
    /// </summary>
    /// <typeparam name="T">The content type to call <paramref name="handleRequest"/> with.</typeparam>
    /// <param name="process">The process to assign the handler function to.</param>
    /// <param name="handleRequest">The function to call when an <see cref="EventSubWebhookRequest"/> with content type <typeparamref name="T"/> is recieved.</param>
    /// <returns>A new <see cref="ProcessWebhookRequest"/> with the handler function added.</returns>
    public static ProcessWebhookRequest Map<T>(this ProcessWebhookRequest process, Func<T, CancellationToken, ValueTask> handleRequest)
        where T : IWebhookRequestContent
        => async (request, ct) =>
        {
            Result<IWebhookRequestContent> result = await process(request, ct);
            return await result.Match<ValueTask<Result<IWebhookRequestContent>>>(
                onError: e => ValueTask.FromResult<Result<IWebhookRequestContent>>(e),
                onValid: async content =>
                {
                    if (content is T typedContent)
                        await handleRequest(typedContent, ct);
                    return new Result<IWebhookRequestContent>(content);
                });
        };

    /// <summary>
    /// Assign a function that is called when an error occurs during request processing.
    /// </summary>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebhookRequest, Func{T, CancellationToken, ValueTask})"/></param>
    /// <param name="handleError">The function to call when an <see cref="Error"/> is returned from the pipeline.</param>
    /// <returns><inheritdoc cref="Map{T}(ProcessWebhookRequest, Func{T, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebhookRequest MapError(this ProcessWebhookRequest process, Func<Error, CancellationToken, ValueTask> handleError)
        => async (request, ct) =>
        {
            Result<IWebhookRequestContent> result = await process(request, ct);
            return await result.Match<ValueTask<Result<IWebhookRequestContent>>>(
                onError: async e =>
                {
                    await handleError(e, ct);
                    return e;
                },
                onValid: content => ValueTask.FromResult(new Result<IWebhookRequestContent>(content))
                );
        };

    /// <summary>
    /// Assign a function to call when an <see cref="IEventSubNotification"/> of type <typeparamref name="T"/> is received.
    /// </summary>
    /// <typeparam name="T">The notification type to call <paramref name="handleNotification"/> for.</typeparam>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebhookRequest, Func{T, CancellationToken, ValueTask})"/></param>
    /// <param name="handleNotification">The function to call when a notification of type <typeparamref name="T"/> is received.</param>
    /// <returns><inheritdoc cref="Map{T}(ProcessWebhookRequest, Func{T, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebhookRequest MapNotification<T>(this ProcessWebhookRequest process, Func<T, CancellationToken, ValueTask> handleNotification)
        where T : IEventSubNotification
        => process.Map<NotificationRequestContent>(
            (notificationContent, ct) => notificationContent.Notification is T notification
                ? handleNotification(notification, ct)
                : ValueTask.CompletedTask
            );

    /// <summary>
    /// Assign a function to call when a subscription is revoked.
    /// </summary>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebhookRequest, Func{T, CancellationToken, ValueTask})"/></param>
    /// <param name="handleSubscriptionRevoked">The function to call when a subscription is revoked.</param>
    /// <returns><inheritdoc cref="Map{T}(ProcessWebhookRequest, Func{T, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebhookRequest MapSubscriptionRevoked(this ProcessWebhookRequest process, Func<IEventSubSubscription, CancellationToken, ValueTask> handleSubscriptionRevoked)
        => process.Map<RevocationRequestContent>((revocationContent, ct) => handleSubscriptionRevoked(revocationContent.Subscription, ct));

    /// <summary>
    /// Assign a function to call when a subscription callback verification request is received.
    /// </summary>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebhookRequest, Func{T, CancellationToken, ValueTask})"/></param>
    /// <param name="handleCallbackVerification">The function to call when a callback verification request is received.</param>
    /// <returns><inheritdoc cref="Map{T}(ProcessWebhookRequest, Func{T, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebhookRequest MapCallbackVerification(this ProcessWebhookRequest process, Func<IEventSubSubscription, string, CancellationToken, ValueTask> handleCallbackVerification)
        => process.Map<CallbackVerificationRequestContent>((callbackVerificationContent, ct) => handleCallbackVerification(callbackVerificationContent.Subscription, callbackVerificationContent.Challenge, ct));
}
