using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Contains a list of deleted videos.
/// </summary>
public record DeleteVideosResponse
{
    /// <summary>
    /// The list of videos ids that were deleted.
    /// </summary>
    public required string[] Data { get; init; }
}
