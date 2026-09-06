namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Contains a list of active streams that a specific user follows.
/// </summary>
public record GetFollowedStreamsResponseContent
    : IPageableResponse
{
    /// <summary>
    /// The list of active followed streams.
    /// </summary>
    public required TwitchStream[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
