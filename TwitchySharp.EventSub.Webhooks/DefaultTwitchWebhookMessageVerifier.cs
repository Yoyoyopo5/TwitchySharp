using System.Security.Cryptography;
using System.Text;

namespace TwitchySharp.EventSub.Webhooks;

/// <summary>
/// Default implementation for verifying Twitch webhook requests.
/// <para>
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#verifying-the-event-message">Verifying The Event Message</see> for more information.
/// </para>
/// </summary>
/// <param name="secret">
/// The secret Twitch used to sign the request. 
/// This is the same secret that was passed to Twitch when creating the EventSub webhook subscription.
/// </param>
public class DefaultTwitchWebhookMessageVerifier(
    byte[] secret
    ) : ITwitchWebhookMessageVerifier, IDisposable
{
    private readonly HMACSHA256 _hmac = new(secret);

    /// <summary>
    /// Dispose the object. This disposes the internal <see cref="HMACSHA256"/> object.
    /// </summary>
    public void Dispose()
        => _hmac.Dispose();

    /// <summary>
    /// Determines whether the message body and headers originated from Twitch.
    /// Process defined in <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#verifying-the-event-message">Verifying The Event Message</see>.
    /// </summary>
    /// <param name="requestHeader">The headers of the webhook HTTP request.</param>
    /// <param name="body">The raw body of the webhook HTTP request.</param>
    /// <returns>A <see langword="bool"/> indicating whether the request came from Twitch.</returns>
    public bool IsValid(EventSubWebhookRequestHeader requestHeader, string body)
        => CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(requestHeader.TwitchEventsubMessageSignature),
            Encoding.UTF8.GetBytes(
                "sha256=" +
                Convert.ToHexString(
                    _hmac.ComputeHash(
                        Encoding.UTF8.GetBytes(
                            requestHeader.TwitchEventsubMessageId +
                            requestHeader.TwitchEventsubMessageTimestamp +
                            body)
                        )
                    )
                )
            );
}
