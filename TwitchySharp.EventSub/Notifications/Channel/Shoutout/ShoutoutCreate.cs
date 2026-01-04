using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Notifications.Channel.Shoutout;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ShoutoutCreate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshoutoutcreate">Shoutout Create</see> for more information.
/// </remarks>
public record ShoutoutCreateNotification : EventSubNotification<ShoutoutCreateEvent, ShoutoutCreateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ShoutoutCreate"/>.
/// </summary>
public record ShoutoutCreateCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ShoutoutCreate"/> event.
/// </summary>
public record ShoutoutCreateEvent : ShoutoutEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required string ToBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required string ToBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required string ToBroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator that sent the shoutout.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that sent the shoutout.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that sent the shoutout.
    /// </summary>
    public required string ModeratorUserName { get; init; }
    /// <summary>
    /// The date and time when the broadcaster may send another shoutout.
    /// </summary>
    public required DateTimeOffset CooldownEndsAt { get; init; }
    /// <summary>
    /// The date and time when the broadcaster may send another shoutout to the same broadcaster.
    /// </summary>
    public required DateTimeOffset TargetCooldownEndsAt { get; init; }
}
