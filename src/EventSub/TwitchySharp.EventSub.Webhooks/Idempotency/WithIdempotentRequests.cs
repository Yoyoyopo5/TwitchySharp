using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Idempotency;

public record IdempotencyError(WebhookMessageId RepeatedMessageId) : Error("A message id was repeated.");

public static class ProcessWebhookRequestExtensions
{
    /// <summary>
    /// Configure an EventSub webhook processing pipeline to return an <see cref="IdempotencyError"/> if a <see cref="WebhookMessageId"/> is repeated.
    /// </summary>
    /// <remarks>
    /// This does not serialize webhook requests, meaning race conditions are still possible.
    /// </remarks>
    /// <param name="pipeline">The webhook processing pipeline to add idempotent requests to.</param>
    /// <param name="isRepeated">The function to determine if a message id is repeated.</param>
    /// <returns>A new <see cref="ProcessWebhookRequest"/> pipeline configured to return an <see cref="IdempotencyError"/> for a repeated <see cref="WebhookMessageId"/>.</returns>
    public static ProcessWebhookRequest WithIdempotentRequests(
        this ProcessWebhookRequest pipeline,
        Func<WebhookMessageId, CancellationToken, ValueTask<bool>> isRepeated
        )
        => pipeline.With(next => async (request, ct) => await isRepeated(request.Header.TwitchEventsubMessageId, ct) ? new Validation<WebhookRequestContent>(new IdempotencyError(request.Header.TwitchEventsubMessageId)) : await next(request, ct));
}
