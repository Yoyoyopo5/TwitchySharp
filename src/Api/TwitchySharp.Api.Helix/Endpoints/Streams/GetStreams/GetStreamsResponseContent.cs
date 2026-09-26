namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Contains a list of found active streams.
/// </summary>
public record GetStreamsResponseContent
    : IPageableResponse
{
    /// <summary>
    /// The active streams that matched the request query.
    /// </summary>
    public required TwitchStream[] Data { get; init; }
    ///<inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
