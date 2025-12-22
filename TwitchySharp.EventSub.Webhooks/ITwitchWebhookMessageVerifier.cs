using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks;
public interface ITwitchWebhookMessageVerifier
{
    ValueTask<bool> IsValid(EventSubWebhookRequestHeader requestHeader, string body, CancellationToken ct = default);
}
