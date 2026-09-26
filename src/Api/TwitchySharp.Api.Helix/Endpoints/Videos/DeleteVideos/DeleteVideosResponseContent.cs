
namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Contains a list of deleted videos.
/// </summary>
public record DeleteVideosResponseContent
{
    /// <summary>
    /// The list of videos ids that were deleted.
    /// </summary>
    public required VideoId[] Data { get; init; }
}
