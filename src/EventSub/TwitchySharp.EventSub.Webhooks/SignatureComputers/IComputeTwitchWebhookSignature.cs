namespace TwitchySharp.EventSub.Webhooks.SignatureComputers;

/// <summary>
/// Computes the Twitch EventSub webhook signature for verification purposes.
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#verifying-the-event-message">Verifying the Event Message</see> for more information on how to compute the hash.
/// </summary>
public interface IComputeTwitchWebhookSignature
{
    /// <summary>
    /// <inheritdoc cref="IComputeTwitchWebhookSignature"/>
    /// </summary>
    /// <param name="secret">The secret that was used to create the webhook, in UTF8 encoded bytes.</param>
    /// <param name="messageId">The id of the webhook message, as indicated by the <c>Twitch-Eventsub-Message-Id</c> header.</param>
    /// <param name="timestamp">The UTC timestamp of the webhook message in RFC3339 format, as indicated by the <c>Twitch-Eventsub-Message-Timestamp</c> header.</param>
    /// <param name="body">The raw body of the webhook message.</param>
    /// <returns>
    /// A <see langword="byte"/>[] representing the computed signature of the message in UTF8 encoded bytes. 
    /// This should match the value of the <c>Twitch-Eventsub-Message-Signature</c> header in the original webhook message.
    /// </returns>
    ValueTask<byte[]> ComputeSignature(byte[] secret, string messageId, string timestamp, string body, CancellationToken ct = default);
    /// <inheritdoc cref="ComputeSignature(byte[], string, string, string, CancellationToken)"/>
    ValueTask<byte[]> ComputeSignature(byte[] secret, string messageId, string timestamp, Stream body, CancellationToken ct = default);
}
