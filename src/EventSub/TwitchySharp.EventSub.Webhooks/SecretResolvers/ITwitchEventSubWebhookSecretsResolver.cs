using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks.SecretResolvers;

public interface ITwitchEventSubWebhookSecretsResolver
{
    public ValueTask<string> GetSecret(EventSubWebhookRequestHeader requestHeaders, string body, CancellationToken ct = default);
    public ValueTask<string> GetSecret(EventSubWebhookRequestHeader requestHeaders, Stream body, CancellationToken ct = default);
}
