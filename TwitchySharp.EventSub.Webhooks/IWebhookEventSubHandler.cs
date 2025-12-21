using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Webhooks.Responses;

namespace TwitchySharp.EventSub.Webhooks;
public interface IWebhookEventSubHandler
{
    ValueTask<RevocationResponseData> OnSubscriptionRevoked(EventSubSubscription revokedSubscription);
    ValueTask<NotificationResponseData> OnNotified(IEventSubNotification notification);
}
