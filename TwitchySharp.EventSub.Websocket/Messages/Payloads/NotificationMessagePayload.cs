using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;

namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;
public class NotificationMessagePayload : IEventSubNotification
{
    public required EventSubSubscription Subscription { get; init; }
    public required JsonElement Event { get; init; }
}
