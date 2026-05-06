using System.Text.Json;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public class TwitchEventSubWebhooksOptions
{
    public JsonSerializerOptions? JsonSerializerOptions { get; set; }
    public IReadOnlyDictionary<EventSubSubscriptionType, Type>? NotificationTypes { get; set; }
}
