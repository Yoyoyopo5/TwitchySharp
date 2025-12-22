using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks;

/// <summary>
/// An implementation of <see cref="ITwitchWebhookMessageVerifier"/> that uses a fixed secret for all verifications.
/// Use this if you use a single secret for all your EventSub webhook subscriptions.
/// </summary>
/// <param name="secret">The fixed secret to use when computing signatures and verifying webhook requests.</param>
/// <param name="signatureComputer">The signature computer to use. Defaults to <see cref="DefaultTwitchWebhookCrypto"/> if <see langword="null"/>.</param>
public class FixedSecretTwitchWebhookMessageVerifier(
    string secret, 
    IComputeTwitchWebhookSignature? signatureComputer = null) 
    : ITwitchWebhookMessageVerifier
{
    private readonly byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
    private readonly IComputeTwitchWebhookSignature _signatureComputer = signatureComputer ?? new DefaultTwitchWebhookCrypto();

    public async ValueTask<bool> IsValid(EventSubWebhookRequestHeader requestHeaders, string body, CancellationToken ct = default)
    {
        byte[] computedHash = await _signatureComputer.ComputeSignature(
                secretBytes,
                requestHeaders.TwitchEventsubMessageId,
                requestHeaders.TwitchEventsubMessageTimestamp,
                body,
                ct
                );
        byte[] expectedHash = Encoding.UTF8.GetBytes(requestHeaders.TwitchEventsubMessageSignature);
        return CryptographicOperations.FixedTimeEquals(expectedHash, computedHash);
    }

    public async ValueTask<bool> IsValid(EventSubWebhookRequestHeader requestHeaders, Stream body, CancellationToken ct = default)
    {
        byte[] computedHash = await _signatureComputer.ComputeSignature(
                secretBytes,
                requestHeaders.TwitchEventsubMessageId,
                requestHeaders.TwitchEventsubMessageTimestamp,
                body,
                ct
                );
        byte[] expectedHash = Encoding.UTF8.GetBytes(requestHeaders.TwitchEventsubMessageSignature);
        return CryptographicOperations.FixedTimeEquals(expectedHash, computedHash);
    }
}
