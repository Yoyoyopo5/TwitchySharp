using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Requests;

namespace TwitchySharp.EventSub.Webhooks;
public interface IEventSubWebhookMessageProcessor
{
    ValueTask HandleRequest(EventSubWebhookRequestHeader requestHeader, Stream bodyStream);
    ValueTask HandleRequest(EventSubWebhookRequestHeader requestHeader, string body);

    ValueTask Notification(IEventSubNotification notification);
    ValueTask CallbackVerification(string challenge, EventSubSubscription subscription);
    ValueTask Revocation(EventSubSubscription subscription);
}
