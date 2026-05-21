using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelBan"/> event.
/// </summary>
public record ChannelBanEvent
{
    /// <summary>
    /// The id of the user who was banned or timed out.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who was banned or timed out.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who was banned or timed out.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the user was banned or timed out.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the user was banned or timed out.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the user was banned or timed out.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator (or the broadcaster) who issued the ban or timeout.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator (or the broadcaster) who issued the ban or timeout.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator (or the broadcaster) who issued the ban or timeout.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
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
