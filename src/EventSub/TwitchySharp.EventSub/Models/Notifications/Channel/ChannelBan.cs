using System.Text.Json.Serialization;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Models.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelBan"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelban">Channel Ban</see> for more information.
/// </remarks>
public record ChannelBanNotification : EventSubNotification<ChannelBanEvent, ChannelBanCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelBan"/>.
/// </summary>
public record ChannelBanCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelBan"/> event.
/// </summary>
public record ChannelBanEvent : IHaveBroadcaster, IHaveModerator, IHaveUser
{
    /// <summary>
    /// The id of the user who was banned or timed out.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who was banned or timed out.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who was banned or timed out.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the user was banned or timed out.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the user was banned or timed out.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the user was banned or timed out.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator (or the broadcaster) who issued the ban or timeout.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator (or the broadcaster) who issued the ban or timeout.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator (or the broadcaster) who issued the ban or timeout.
    /// </summary>
    public required string ModeratorUserName { get; init; }
    /// <summary>
    /// The reason given for the ban or timeout by the moderator.
    /// </summary>
    public required string Reason { get; init; } // This may be optional, or defaults to empty string.
    /// <summary>
    /// The date and time when the ban or timeout occurred.
    /// </summary>
    public required DateTimeOffset BannedAt { get; init; }
    /// <summary>
    /// The date and time when the timeout ends.
    /// This is <see langword="null"/> if <see cref="IsPermanent"/> is <see langword="true"/>.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? EndsAt { get; init; }
    /// <summary>
    /// Indicates whether the ban is permanent.
    /// If this is <see langword="false"/>, a timeout was issued.
    /// </summary>
    public required bool IsPermanent { get; init; }
}
