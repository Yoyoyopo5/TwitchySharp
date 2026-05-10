using System.Buffers;
using System.Security.Cryptography;
using System.Text;

namespace TwitchySharp.EventSub.Webhooks;

internal static class EventSubWebhookCrypto
{
    private static byte[] FormatSignature(ReadOnlySpan<byte> hash)
        => Encoding.UTF8.GetBytes("sha256=" + Convert.ToHexString(hash));

    /// <summary>
    /// Default implementation for computing Twitch Webhook signatures.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#verifying-the-event-message">Verifying the Event Message</see> for more information on how the signature is computed.
    /// </summary>
    public static async ValueTask<byte[]> ComputeSignature(byte[] secretBytes, string messageId, string timestamp, Stream body, CancellationToken ct = default)
    {
        using IncrementalHash hmac = IncrementalHash.CreateHMAC(HashAlgorithmName.SHA256, secretBytes);
        hmac.AppendData(Encoding.UTF8.GetBytes(messageId).AsSpan());
        hmac.AppendData(Encoding.UTF8.GetBytes(timestamp).AsSpan());
        byte[] buffer = ArrayPool<byte>.Shared.Rent(2048);
        try
        {
            int bytesRead;
            while((bytesRead = await body.ReadAsync(buffer, ct)) > 0)
            {
                hmac.AppendData(buffer.AsSpan(0, bytesRead));
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
        // Span<byte> hash; TODO: Can use in > C# 12
        // hmac.GetHashAndReset(hash);
        byte[] hash = hmac.GetHashAndReset();
        return FormatSignature(hash);
    }
}
