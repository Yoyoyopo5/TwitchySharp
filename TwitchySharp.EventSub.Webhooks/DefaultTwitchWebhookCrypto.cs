using System.Security.Cryptography;
using System.Text;

namespace TwitchySharp.EventSub.Webhooks;

/// <summary>
/// Default implementation for computing Twitch Webhook signatures.
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#verifying-the-event-message">Verifying the Event Message</see> for more information on how the signature is computed.
/// </summary>
public class DefaultTwitchWebhookCrypto : IComputeTwitchWebhookSignature
{
    private static byte[] FormatSignature(byte[] hash)
        => Encoding.UTF8.GetBytes("sha256=" + Convert.ToHexString(hash));

    public async ValueTask<byte[]> ComputeSignature(byte[] secretBytes, string messageId, string timestamp, Stream body, CancellationToken ct = default)
    {
        using HMACSHA256 hmac = new(secretBytes);
        using CryptoStream cryptoStream = new(Stream.Null, hmac, CryptoStreamMode.Write);
        await cryptoStream.WriteAsync(Encoding.UTF8.GetBytes(messageId), ct);
        await cryptoStream.WriteAsync(Encoding.UTF8.GetBytes(timestamp), ct);
        await body.CopyToAsync(cryptoStream, ct);
        await cryptoStream.FlushFinalBlockAsync(ct);
        return FormatSignature(hmac.Hash!);
    }

    public ValueTask<byte[]> ComputeSignature(byte[] secretBytes, string messageId, string timestamp, string body, CancellationToken ct = default)
    {
        using IncrementalHash ih = IncrementalHash.CreateHMAC(HashAlgorithmName.SHA256, secretBytes);
        ih.AppendData(Encoding.UTF8.GetBytes(messageId));
        ih.AppendData(Encoding.UTF8.GetBytes(timestamp));
        ih.AppendData(Encoding.UTF8.GetBytes(body));
        return ValueTask.FromResult(FormatSignature(ih.GetHashAndReset()));
    }
}
