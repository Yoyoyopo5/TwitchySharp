using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of a channel's banned or timed-out users.
/// </summary>
public record GetBannedUsersResponse
{
    /// <summary>
    /// A list of the channel's banned and timed-out users.
    /// </summary>
    public required BannedUser[] Data { get; init; }
    /// <summary>
    /// <inheritdoc cref="Models.Pagination"/>
    /// </summary>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about an individual user's channel ban.
/// </summary>
public record BannedUser
{
    /// <summary>
    /// The id of the banned or timed-out user.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the banned or timed-out user.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the banned or timed-out user.
    /// </summary>
    public required string UserName { get; init; }
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
    public required string ModeratorId { get; init; }
    /// <summary>
    /// The login (username) of the moderator who banned or timed out the user.
    /// </summary>
    public required string ModeratorLogin { get; init; }
    /// <summary>
    /// The display name of the moderator who banned or timed out the user.
    /// </summary>
    public required string ModeratorName { get; init; }
}
