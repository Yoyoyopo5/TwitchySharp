using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Webhooks.Responses;

namespace TwitchySharp.EventSub.Webhooks;

public interface IWebhookCallbackVerifier
{
    public ValueTask<CallbackVerificationResponseData> VerifyCallback(string challenge, CancellationToken ct = default);
}

public class DefaultWebhookCallbackVerifier : IWebhookCallbackVerifier
{
    public ValueTask<CallbackVerificationResponseData> VerifyCallback(string challenge, CancellationToken ct = default)
        => ValueTask.FromResult(new CallbackVerificationResponseData
        {
            Challenge = challenge
        });
}
