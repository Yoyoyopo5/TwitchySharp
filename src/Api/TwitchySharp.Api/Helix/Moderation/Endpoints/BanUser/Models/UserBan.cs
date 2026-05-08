using System;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains information about a specific ban or time-out on a channel.
/// </summary>
public record UserBan
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the user is banned or timed out from.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The user id of the moderator who issued the ban or time-out.
    /// </summary>
    public required UserId ModeratorId { get; init; }
    /// <summary>
    /// The user id of the user that was banned or timed out.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The date and time that the user was banned or timed out.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The date and time that the time-out will expire.
    /// This is <see langword="null"/> if the action was a time-out.
    /// </summary>
    public DateTimeOffset? EndTime { get; init; } // Don't think we need converter here because field should be null
}
