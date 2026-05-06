using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelUnban"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelunban">Channel Unban</see> for more information.
/// </remarks>
public record ChannelUnbanNotification : EventSubNotification<ChannelUnbanEvent, ChannelUnbanCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelUnban"/>.
/// </summary>
public record ChannelUnbanCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelUnban"/> event.
/// </summary>
public record ChannelUnbanEvent : IHaveBroadcaster, IHaveModerator, IHaveUser
{
    /// <summary>
    /// The id of the user who was unbanned or untimedout.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who was unbanned or untimedout.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who was unbanned or untimedout.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the user was unbanned or untimedout.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the user was unbanned or untimedout.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the user was unbanned or untimedout.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator (or the broadcaster) who issued the unban or untimeout.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator (or the broadcaster) who issued the unban or untimeout.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator (or the broadcaster) who issued the unban or untimeout.
    /// </summary>
    public required string ModeratorUserName { get; init; }
}
