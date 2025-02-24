using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications;

namespace TwitchySharp.EventSub.Webhooks;
public interface IWebhookEventSubHandler
{
    ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription);
    ValueTask OnNotified(IEventSubNotification notification);
    ValueTask OnException(Exception ex);
}
