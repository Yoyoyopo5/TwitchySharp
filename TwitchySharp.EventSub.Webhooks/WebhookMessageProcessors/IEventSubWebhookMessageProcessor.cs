using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Requests;
using TwitchySharp.EventSub.Webhooks.Responses;

namespace TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;
public interface IEventSubWebhookMessageProcessor
{
    ValueTask<WebhookResponseData> HandleRequest(EventSubWebhookRequestHeader requestHeader, Stream bodyStream, CancellationToken ct = default);
    ValueTask<WebhookResponseData> HandleRequest(EventSubWebhookRequestHeader requestHeader, string body, CancellationToken ct = default);
}
