using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications;

namespace TwitchySharp.EventSub.Webhooks.Requests;

internal abstract record WebhookRequestData
{
    public required EventSubSubscription Subscription { get; init; }
}
