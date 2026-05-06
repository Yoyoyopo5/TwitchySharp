using TwitchySharp.EventSub.Webhooks.Responses;

namespace TwitchySharp.EventSub.Webhooks.CallbackVerifiers;

public interface IWebhookCallbackVerifier
{
    ValueTask<CallbackVerificationResponseData> VerifyCallback(string challenge, CancellationToken ct = default);
}
