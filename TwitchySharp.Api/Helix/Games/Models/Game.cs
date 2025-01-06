using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Games;

/// <summary>
/// Contains data about a specific game (category) on Twitch.
/// </summary>
public record Game
{
    /// <summary>
    /// An id that identifies the category or game.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The category or game's name.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// A url to the category or game's box art.
    /// </summary>
    public required ImageUrlTemplate BoxArtUrl { get; init; }
    /// <summary>
    /// The ID that <see href="https://www.igdb.com/">IGDB</see> uses to identify this game. 
    /// If the IGDB ID is not available to Twitch, this is an empty string.
    /// </summary>
    public required string IgdbId { get; init; }
}
