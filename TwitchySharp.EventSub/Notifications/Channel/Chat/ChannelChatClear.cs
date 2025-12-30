using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatClear"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatclear">Channel Chat Clear</see> for more information.
/// </remarks>
public record ChannelChatClearNotification : EventSubNotification<ChannelChatClearEvent, ChannelChatClearCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatClear"/>.
/// </summary>
public record ChannelChatClearCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Channel Chat Clear notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The id of the user to read chat as.
    /// </summary>
    public required string UserId { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatClear"/> event.
/// </summary>
public record ChannelChatClearEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that had their chat cleared.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that had their chat cleared.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that had their chat cleared.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
}
