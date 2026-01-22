namespace TwitchySharp.Api.Helix.Games;

public record GetTopGamesResponse
    : IPageableResponse
{
    /// <summary>
    /// The list of top games.
    /// </summary>
    public required Game[] Data { get; init; }
    /// <summary>
    /// Contains a cursor used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}


