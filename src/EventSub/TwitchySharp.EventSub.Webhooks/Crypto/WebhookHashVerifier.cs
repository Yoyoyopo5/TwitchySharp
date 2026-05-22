using System.Security.Cryptography;
using System.Text;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Crypto;

public delegate ValueTask<Validation<Unit>> VerifyWebhookHash(EventSubSubscription subscription, EventSubWebhookRequest request, CancellationToken ct);

public class WebhookHashVerifier
{
    public record VerificationError(string Message, EventSubSubscription Subscription) : Error(Message);

    public static VerifyWebhookHash Create(ResolveWebhookSecret resolveSecret)
        => async (subscription, request, ct) => await resolveSecret(subscription, ct) is not WebhookSecret secret
            ? new VerificationError("The webhook for the subscription resolved to null.", subscription)
            : await VerifySignature(secret, request, ct)
            ? new Unit()
            : new VerificationError("The webhook request did not have the expected hash for the resolved secret. This may mean the request did not originate from Twitch.", subscription);

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
