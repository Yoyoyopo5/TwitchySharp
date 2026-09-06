namespace TwitchySharp.Api.Helix.Games;
/// <summary>
/// Contains a list of Twitch categories.
/// </summary>
public record GetGamesResponseContent
{
    /// <summary>
    /// The list of categories and games.
    /// </summary>
    public required Game[] Data { get; init; }
}
