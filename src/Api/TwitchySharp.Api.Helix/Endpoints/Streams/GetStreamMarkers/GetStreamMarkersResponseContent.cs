namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Contains a list of stream markers.
/// </summary>
public record GetStreamMarkersResponseContent
    : IPageableResponse
{
    /// <summary>
    /// The list of markers grouped by the user that created the marks.
    /// </summary>
    public required UserStreamMarkers[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
