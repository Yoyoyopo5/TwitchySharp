using System.Security.Cryptography;
using System.Text;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Crypto;

/// <summary>
/// A function that determines if a given <see cref="EventSubWebhookRequestHeader.TwitchEventsubMessageSignature"/> is valid for the request (i.e. it originated from Twitch).
/// </summary>
/// <param name="subscription">The subscription that the request is for.</param>
/// <param name="request">The webhook request to verify.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="Result"/> which, if not in the errored state, indicates a valid request hash.</returns>
public delegate ValueTask<Result> VerifyWebhookHash(IEventSubSubscription subscription, EventSubWebhookRequest request, CancellationToken ct);

/// <summary>
/// Creation helpers for <see cref="VerifyWebhookHash"/>.
/// </summary>
public static class VerifyWebhookHashExtensions
{
    /// <summary>
    /// A <see cref="WebhookSecret"/> resolved to <see langword="null"/> for the <paramref name="Subscription"/>.
    /// </summary>
    /// <param name="Subscription">The subscription that resolved to a <see langword="null"/> <see cref="WebhookSecret"/>.</param>
    public record MissingSecretError(IEventSubSubscription Subscription)
        : Error("No webhook secret was found for the subscription type.");
    /// <summary>
    /// Hash verification failed for the request.
    /// </summary>
    public record VerificationFailedError()
        : Error("The webhook request did not have the expected hash for the resolved secret. This may mean the request did not originate from Twitch.");

    extension (VerifyWebhookHash v)
    {
        /// <summary>
        /// Create a <see cref="VerifyWebhookHash"/> that uses <paramref name="resolveSecret"/> to get
        /// a <see cref="WebhookSecret"/> to verify requests for each <see cref="IEventSubSubscription"/>.
        /// </summary>
        /// <param name="resolveSecret">A function returning a <see cref="WebhookSecret"/> for a given <see cref="IEventSubSubscription"/>.</param>
        /// <returns>A new verifier function.</returns>
        public static VerifyWebhookHash UsingSecret(ResolveWebhookSecret resolveSecret)
            => async (subscription, request, ct) => await resolveSecret(subscription, ct) is not WebhookSecret secret
            ? new MissingSecretError(subscription)
            : await VerifySignature(secret, request, ct)
            ? new Result()
            : new VerificationFailedError();

        /// <summary>
        /// Create a <see cref="VerifyWebhookHash"/> that uses a fixed secret for every request.
        /// </summary>
        /// <remarks>
        /// Prefer using <see cref="ResolveWebhookSecret"/> with subscription-specific secrets for enhanced security.
        /// </remarks>
        /// <param name="secret">The secret to use.</param>
        /// <returns>A new verifier function.</returns>
        public static VerifyWebhookHash UsingSecret(WebhookSecret secret)
            => UsingSecret((_, _) => ValueTask.FromResult<WebhookSecret?>(secret));
    }

    private static async ValueTask<bool> VerifySignature(
        WebhookSecret secret,
        EventSubWebhookRequest request,
        CancellationToken ct
        )
    {
        byte[] computedHash = await EventSubWebhookCrypto.ComputeSignature(
            Encoding.UTF8.GetBytes(secret),
            request.Header.TwitchEventsubMessageId,
            request.Header.TwitchEventsubMessageTimestamp,
            request.Content,
            ct
            );
        byte[] expectedHash = Encoding.UTF8.GetBytes(request.Header.TwitchEventsubMessageSignature);
        return CryptographicOperations.FixedTimeEquals(expectedHash, computedHash);
    }
}
