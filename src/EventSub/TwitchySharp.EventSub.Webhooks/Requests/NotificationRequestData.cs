using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Notifications;

namespace TwitchySharp.EventSub.Webhooks.Requests;

internal record NotificationRequestData : WebhookRequestData
{
    public required IEventSubNotification Notification { get; init; }
}
