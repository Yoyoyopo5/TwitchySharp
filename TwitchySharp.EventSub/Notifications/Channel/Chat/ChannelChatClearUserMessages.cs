using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatClearUserMessages"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatclear_user_messages">Channel Chat Clear User Messages</see> for more information.
/// </remarks>
public record ChannelChatClearUserMessagesNotification : EventSubNotification<ChannelChatClearUserMessagesEvent, ChannelChatClearUserMessagesCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatClearUserMessages"/>.
/// </summary>
public record ChannelChatClearUserMessagesCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Channel Chat Clear User Messages notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The id of the user to read chat as.
    /// </summary>
    public required string UserId { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatClearUserMessages"/> event.
/// </summary>
public record ChannelChatClearUserMessagesEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the user's chat messages were cleared.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the user's chat messages were cleared.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the user's chat messages were cleared.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the user whose chat messages were cleared.
    /// </summary>
    public required string TargetUserId { get; init; }
    /// <summary>
    /// The display name of the user whose chat messages were cleared.
    /// </summary>
    public required string TargetUserName { get; init; }
    /// <summary>
    /// The login (username) of the user whose chat messages were cleared.
    /// </summary>
    public required string TargetUserLogin { get; init; }
}
