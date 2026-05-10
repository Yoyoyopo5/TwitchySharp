using TwitchySharp.EventSub.Webhooks.Responses;

namespace TwitchySharp.EventSub.Webhooks.CallbackVerifiers;

/// <summary>
/// Create a <see cref="CallbackVerificationResponseData"/> from a given webhook callback challenge string.
/// </summary>
/// <param name="challenge">The webhook challenge provided by Twitch.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="ValueTask"/> containing the response data (content) to respond to the callback request with.</returns>
public delegate ValueTask<CallbackVerificationResponseData> VerifyWebhookCallback(string challenge, CancellationToken ct);

/// <summary>
/// Contains the default implementation for <see cref="VerifyWebhookCallback"/>.
/// </summary>
public static class WebhookCallbackVerifier
{
    /// <summary>
    /// Creates a <see cref="CallbackVerificationResponseData"/> containing the <paramref name="challenge"/>.
    /// </summary>
    /// <remarks>
    /// The default <see cref="VerifyWebhookCallback"/> implementation.
    /// </remarks>
    /// <param name="challenge"><inheritdoc cref="VerifyWebhookCallback"/></param>
    /// <param name="ct"><inheritdoc cref="VerifyWebhookCallback"/></param>
    /// <returns><inheritdoc cref="VerifyWebhookCallback"/></returns>
    public static ValueTask<CallbackVerificationResponseData> VerifyCallback(string challenge, CancellationToken ct = default)
        => ValueTask.FromResult(new CallbackVerificationResponseData
        {
            Challenge = challenge
        });
}
