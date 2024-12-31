using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains information about a newly banned or timed out user.
/// </summary>
public record BanUserResponse
{
    /// <summary>
    /// A list that contains the information about single user that was banned or timed out.
    /// </summary>
    public required UserBan[] Data { get; init; }
}

/// <summary>
/// Contains information about a specific ban or time-out on a channel.
/// </summary>
public record UserBan
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the user is banned or timed out from.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The user id of the moderator who issued the ban or time-out.
    /// </summary>
    public required string ModeratorId { get; init; }
    /// <summary>
    /// The user id of the user that was banned or timed out.
    /// </summary>
    public required string UserId { get; init; }
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
