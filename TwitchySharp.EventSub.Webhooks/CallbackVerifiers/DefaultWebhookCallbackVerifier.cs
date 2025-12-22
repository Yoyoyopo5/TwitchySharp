using TwitchySharp.EventSub.Webhooks.Responses;

namespace TwitchySharp.EventSub.Webhooks.CallbackVerifiers;

public class DefaultWebhookCallbackVerifier : IWebhookCallbackVerifier
{
    public ValueTask<CallbackVerificationResponseData> VerifyCallback(string challenge, CancellationToken ct = default)
        => ValueTask.FromResult(new CallbackVerificationResponseData
        {
            Challenge = challenge
        });
}
