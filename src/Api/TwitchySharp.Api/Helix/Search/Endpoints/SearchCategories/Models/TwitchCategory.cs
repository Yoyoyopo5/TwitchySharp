using System;

namespace TwitchySharp.Api.Helix.Search;

/// <summary>
/// Contains information about a specific Twitch category (game).
/// </summary>
public record TwitchCategory
{
    /// <summary>
    /// A URL to an image of the game’s box art or streaming category.
    /// </summary>
    public required Uri BoxArtUrl { get; init; }
    /// <summary>
    /// The name of the game or category.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// The id of the category.
    /// </summary>
    public required GameId Id { get; init; }
}
