using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Contains a list of deleted videos.
/// </summary>
public record DeleteVideosResponse
{
    /// <summary>
    /// The list of videos ids that were deleted.
    /// </summary>
    public required VideoId[] Data { get; init; }
}
