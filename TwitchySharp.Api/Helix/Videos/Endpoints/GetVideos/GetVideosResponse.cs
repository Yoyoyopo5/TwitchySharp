namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Contains a list of Twitch videos.
/// </summary>
public record GetVideosResponse
    : IPageableResponse
{
    /// <summary>
    /// The list of published videos that match the request filter criteria.
    /// </summary>
    public required TwitchVideo[] Data { get; init; }
    /// <inheritdoc cref="Api.Pagination"/>
    public required Pagination Pagination { get; init; }
}