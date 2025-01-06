using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Teams;
/// <inheritdoc cref="TwitchTeam"/>
public record GetTeamsResponse
{
    /// <summary>
    /// A list that contains a single team.
    /// </summary>
    public required TwitchTeam[] Data { get; init; }
}

/// <summary>
/// Contains information about a specific team.
/// </summary>
public record TwitchTeam
{
    /// <summary>
    /// The members of the team.
    /// </summary>
    public required TeamMember[] Users { get; init; }
    /// <summary>
    /// A URL to the team’s background image.
    /// </summary>
    public required string BackgroundImageUrl { get; init; }
    /// <summary>
    /// A URL to the team’s banner.
    /// </summary>
    public required string Banner { get; init; }
    /// <summary>
    /// The date and time when the team was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The date and time of the last update to the team.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
    /// <summary>
    /// The team’s description. 
    /// The description may contain formatting such as Markdown, HTML, newline (\n) characters, etc.
    /// </summary>
    public required string Info { get; init; }
    /// <summary>
    /// A URL to a thumbnail image of the team’s logo.
    /// </summary>
    public required string ThumbnailUrl { get; init; }
    /// <summary>
    /// The team’s name.
    /// </summary>
    public required string TeamName { get; init; }
    /// <summary>
    /// The team’s display name.
    /// </summary>
    public required string TeamDisplayName { get; init; }
    /// <summary>
    /// The id of the team.
    /// </summary>
    public required string Id { get; init; }
}

public record TeamMember
{
    public required string UserId { get; init; }
    public required string UserLogin { get; init; }
    public required string UserName { get; init; }
}
