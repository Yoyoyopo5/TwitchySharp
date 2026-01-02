using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Chat;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatUserMessageUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatuser_message_update">Channel Chat User Message Update</see> for more information.
/// </remarks>
public record ChannelChatUserMessageUpdateNotification : EventSubNotification<ChannelChatUserMessageUpdateEvent, ChannelChatUserMessageUpdateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatUserMessageUpdate"/>.
/// </summary>
public record ChannelChatUserMessageUpdateCondition : BroadcasterUserCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatUserMessageUpdate"/> event.
/// </summary>
public record ChannelChatUserMessageUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Channel Chat User Message Update notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) to get Channel Chat User Message Update notifications for.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) to get Channel Chat User Message Update notifications for.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that sent the held message.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the held message.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that sent the held message.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The updated status of the held message.
    /// </summary>
    public required ChannelChatUserMessageUpdateStatus Status { get; init; }
    /// <summary>
    /// The id of the held message.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The held message.
    /// </summary>
    public required ChannelChatMessage Message { get; init; }
}

/// <summary>
/// Contains static definitions for possible user message update statuses.
/// </summary>
/// <param name="Value">The string value of the status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelChatUserMessageUpdateStatus, string>))]
public record ChannelChatUserMessageUpdateStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelChatUserMessageUpdateStatus Approved { get; } = new("approved");
    public static ChannelChatUserMessageUpdateStatus Denied { get; } = new("denied");
    public static ChannelChatUserMessageUpdateStatus Invalid { get; } = new("invalid");
}
