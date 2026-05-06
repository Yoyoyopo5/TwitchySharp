using System.Text.Json;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Interfaces;

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
    /// <summary>
    /// Deserializes a JSON document into a type implementing <see cref="IEventSubNotification"/>.
    /// The type is determined based on the subscription property of the <paramref name="json"/>.
    /// </summary>
    /// <param name="json"><inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/></param>
    /// <returns><inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/></returns>
    IEventSubNotification Deserialize(JsonDocument json);

    /// <summary>
    /// <inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/>
    /// </summary>
    /// <param name="json"><inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/></param>
    /// <param name="subscriptionType"><inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/></param>
    /// <returns><inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/></returns>
    IEventSubNotification Deserialize(JsonElement json, EventSubSubscriptionType subscriptionType);
    /// <summary>
    /// <inheritdoc cref="Deserialize(JsonDocument)"/>
    /// </summary>
    /// <param name="json"><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"/></param>
    /// <returns><inheritdoc cref="Deserialize(JsonElement, EventSubSubscriptionType)"></returns>
    IEventSubNotification Deserialize(JsonElement json);

    /// <summary>
    /// <inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/>
    /// </summary>
    /// <param name="json">A JSON string of the EventSub notification to deserialize.</param>
    /// <param name="subscriptionType"><inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/></param>
    /// <returns><inheritdoc cref="Deserialize(JsonDocument, EventSubSubscriptionType)"/></returns>
    IEventSubNotification Deserialize(string json, EventSubSubscriptionType subscriptionType);
    /// <summary>
    /// <inheritdoc cref="Deserialize(JsonDocument)"/>
    /// </summary>
    /// <param name="json"><inheritdoc cref="Deserialize(string, EventSubSubscriptionType)"/></param>
    /// <returns><inheritdoc cref="Deserialize(string, EventSubSubscriptionType)"/></returns>
    IEventSubNotification Deserialize(string json);
}
