using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Teams;
/// <summary>
/// Contains a list of teams a broadcaster belongs to.
/// </summary>
public record GetChannelTeamsResponse
{
    /// <summary>
    /// The list of teams the broadcaster belongs to.
    /// </summary>
    public required BroadcasterTeam[] Data { get; init; }
}

/// <summary>
/// Contains information about a specific team that a broadcaster belongs to.
/// </summary>
public record BroadcasterTeam
{
    /// <summary>
    /// The user id of the broadcaster.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <inheritdoc cref="TwitchTeam.BackgroundImageUrl"/>
    public required string BackgroundImageUrl { get; init; }
    /// <inheritdoc cref="TwitchTeam.Banner"/>
    public required string Banner { get; init; }
    /// <inheritdoc cref="TwitchTeam.CreatedAt"/>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <inheritdoc cref="TwitchTeam.UpdatedAt"/>
    public required DateTimeOffset UpdatedAt { get; init; }
    /// <inheritdoc cref="TwitchTeam.Info"/>
    public required string Info { get; init; }
    /// <inheritdoc cref="TwitchTeam.ThumbnailUrl"/>
    public required string ThumbnailUrl { get; init; }
    /// <inheritdoc cref="TwitchTeam.TeamName"/>
    public required string TeamName { get; init; }
    /// <inheritdoc cref="TwitchTeam.Name"/>
    public required string TeamDisplayName { get; init; }
    /// <inheritdoc cref="TwitchTeam.Id"/>
    public required string Id { get; init; }
}
