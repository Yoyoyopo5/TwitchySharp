using System;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Teams;

/// <summary>
/// Contains information about a specific team that a broadcaster belongs to.
/// </summary>
public record BroadcasterTeam
{
    /// <summary>
    /// The user id of the broadcaster.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster.
    /// </summary>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
    /// <inheritdoc cref="TwitchTeam.BackgroundImageUrl"/>
    public required Uri BackgroundImageUrl { get; init; }
    /// <inheritdoc cref="TwitchTeam.Banner"/>
    public required Uri Banner { get; init; }
    /// <inheritdoc cref="TwitchTeam.CreatedAt"/>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <inheritdoc cref="TwitchTeam.UpdatedAt"/>
    public required DateTimeOffset UpdatedAt { get; init; }
    /// <inheritdoc cref="TwitchTeam.Info"/>
    public required string Info { get; init; }
    /// <inheritdoc cref="TwitchTeam.ThumbnailUrl"/>
    public required Uri ThumbnailUrl { get; init; }
    /// <inheritdoc cref="TwitchTeam.TeamName"/>
    public required string TeamName { get; init; }
    /// <inheritdoc cref="TwitchTeam.TeamDisplayName"/>
    public required string TeamDisplayName { get; init; }
    /// <inheritdoc cref="TwitchTeam.Id"/>
    public required TeamId Id { get; init; }
}
