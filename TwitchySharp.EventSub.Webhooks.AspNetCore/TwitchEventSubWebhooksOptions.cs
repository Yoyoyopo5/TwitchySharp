using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public class TwitchEventSubWebhooksOptions
{
    public JsonSerializerOptions? JsonSerializerOptions { get; set; }
    public IReadOnlyDictionary<EventSubSubscriptionType, Type>? NotificationTypes { get; set; }
}
