using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Moderator;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelModeratorAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelmoderatoradd">Channel Moderator Add</see> for more information.
/// </remarks>
public record ChannelModeratorAddNotification : EventSubNotification<ChannelModeratorAddEvent, ChannelModeratorAddCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelModeratorAdd"/>.
/// </summary>
public record ChannelModeratorAddCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelModeratorAdd"/> event.
/// </summary>
public record ChannelModeratorAddEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the moderator was added.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the moderator was added.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the moderator was added.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that was added as a moderator.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was added as a moderator.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was added as a moderator.
    /// </summary>
    public required string UserName { get; init; }
}
