using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications;

namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;
internal class NotificationMessagePayload
{
    public required EventSubSubscription Subscription { get; init; }
    public required JsonElement Event { get; init; }
}
