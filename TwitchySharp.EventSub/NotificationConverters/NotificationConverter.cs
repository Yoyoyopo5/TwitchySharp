using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Notifications.Automod;
using TwitchySharp.Shared;
using TwitchySharp.Shared.EventSub.Models;

namespace TwitchySharp.EventSub.NotificationConverters;

/// <summary>
/// Enables conversion between JSON inputs from EventSub notifications and their respective C# instance types.
/// </summary>
public interface INotificationConverter
{
    /// <summary>
    /// Deserializes a JSON document into a type implementing <see cref="IEventSubNotification"/> using a given <see cref="EventSubSubscriptionType"/>.
    /// </summary>
    /// <param name="json">The parsed EventSub notification to deserialize.</param>
    /// <param name="subscriptionType">The subscription type of the EventSub notification.</param>
    /// <returns>An instance of a type that implements <see cref="IEventSubNotification"/>.</returns>
    IEventSubNotification Deserialize(JsonDocument json, EventSubSubscriptionType subscriptionType);
}

/// <summary>
/// The default implementation of <see cref="INotificationConverter"/>.
/// Enables conversion between JSON EventSub notifications and C# instance types using a notification type map.
/// </summary>
/// <param name="notificationTypes">
/// The type map used to deserialize notifications into instances.
/// Type values must implement <see cref="IEventSubNotification"/> or <see cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/> will throw <see cref="InvalidCastException"/>.
/// <para/>
/// If left null, the <see cref="DefaultNotificationTypes"/> map is used.
/// <para/>
/// Leave this null unless you know what you're doing. 
/// You can copy <see cref="DefaultNotificationTypes"/> and add new types if the type you need hasn't been included yet.
/// </param>
public class NotificationConverter(IReadOnlyDictionary<EventSubSubscriptionType, Type>? notificationTypes = null)
    : INotificationConverter
{
    /// <summary>
    /// The default notification type map supplied with TwitchySharp.
    /// Contains key value pairs mapping keys of <see cref="EventSubSubscriptionType"/> to corresponding types implementing <see cref="IEventSubNotification"/>.
    /// </summary>
    public static readonly IReadOnlyDictionary<EventSubSubscriptionType, Type> DefaultNotificationTypes = new Dictionary<EventSubSubscriptionType, Type>()
    {
        { EventSubSubscriptionType.AutomodMessageHold, typeof(AutomodMessageHoldNotification) }
    };

    private JsonSerializerOptions _serializerOptions = JsonConfig.ApiOptions;
    private IReadOnlyDictionary<EventSubSubscriptionType, Type> _notificationTypes = notificationTypes ?? DefaultNotificationTypes;

    /// <summary>
    /// Deserializes a JSON document into a type implementing <see cref="IEventSubNotification"/> using the class' notification type map.
    /// </summary>
    /// <param name="json"><inheritdoc cref="INotificationConverter.Deserialize(JsonDocument, EventSubSubscriptionType)"/></param>
    /// <param name="subscriptionType"><inheritdoc cref="INotificationConverter.Deserialize(JsonDocument, EventSubSubscriptionType)"/></param>
    /// <returns>
    /// <inheritdoc cref="INotificationConverter.Deserialize(JsonDocument, EventSubSubscriptionType)"/>
    /// You can use a switch expression to pattern match this value into any number of distinct instance types.
    /// </returns>
    /// <exception cref="ArgumentException">The <paramref name="json"/> was a null literal value.</exception>
    /// <exception cref="InvalidCastException">The value of the <paramref name="subscriptionType"/> key in this instance's notification type map is not a type that implements <see cref="IEventSubNotification"/>.</exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public IEventSubNotification Deserialize(JsonDocument json, EventSubSubscriptionType subscriptionType)
        => (IEventSubNotification?)json.Deserialize(_notificationTypes[subscriptionType], _serializerOptions) ?? throw new ArgumentException("JSON cannot be null literal.", nameof(json));

}
