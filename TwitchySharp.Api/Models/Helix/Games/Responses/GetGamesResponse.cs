using TwitchySharp.Api.Models.Helix.Games.Models;

namespace TwitchySharp.Api.Models.Helix.Games.Responses;
/// <summary>
/// Contains a list of Twitch categories.
/// </summary>
public record GetGamesResponse
{
    /// <summary>
    /// The list of categories and games.
    /// </summary>
    public required Game[] Data { get; init; }
}
