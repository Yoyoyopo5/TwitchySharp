using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Notifications.EventModels;
using TwitchySharp.Shared;
using TwitchySharp.Shared.EventSub.Models;

namespace TwitchySharp.EventSub.NotificationConverters;

public interface INotificationConverter
{
    IEventSubNotification Convert(JsonDocument json, EventSubSubscriptionType subscriptionType);
}

public class NotificationConverter(IReadOnlyDictionary<EventSubSubscriptionType, Type>? notificationTypes = null)
    : INotificationConverter
{
    public static readonly IReadOnlyDictionary<EventSubSubscriptionType, Type> DefaultNotificationTypes = new Dictionary<EventSubSubscriptionType, Type>()
    {
        { EventSubSubscriptionType.AutomodMessageHold, typeof(AutomodMessageHoldNotification) }
    };

    private JsonSerializerOptions _serializerOptions = JsonConfig.ApiOptions;
    private IReadOnlyDictionary<EventSubSubscriptionType, Type> _notificationTypes = notificationTypes ?? DefaultNotificationTypes;
    public IEventSubNotification Convert(JsonDocument json, EventSubSubscriptionType subscriptionType)
        => (IEventSubNotification?)json.Deserialize(_notificationTypes[subscriptionType], _serializerOptions) ?? throw new ArgumentException("JSON cannot be null literal.");
}
