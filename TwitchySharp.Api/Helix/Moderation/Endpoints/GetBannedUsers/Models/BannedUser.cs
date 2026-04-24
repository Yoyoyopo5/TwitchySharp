using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains information about an individual user's channel ban.
/// </summary>
public record BannedUser
{
    /// <summary>
    /// The id of the banned or timed-out user.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the banned or timed-out user.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the banned or timed-out user.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// If the user was timed out, the date and time when the timeout expires.
    /// If the user was banned, this is <see langword="null"/>.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? ExpiresAt { get; init; }
    /// <summary>
    /// The date and time when the user was banned or timed out.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The reason the user was banned or timed out if the moderator provided one.
    /// Otherwise, an empty string.
    /// </summary>
    public required string Reason { get; init; }
    /// <summary>
    /// The user id of the moderator who banned or timed out the user.
    /// </summary>
    public required UserId ModeratorId { get; init; }
    /// <summary>
    /// The login (username) of the moderator who banned or timed out the user.
    /// </summary>
    public required UserLogin ModeratorLogin { get; init; }
    /// <summary>
    /// The display name of the moderator who banned or timed out the user.
    /// </summary>
    public required UserName ModeratorName { get; init; }
}
