using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Clips;
/// <summary>
/// Contains information related to a newly captured clip.
/// </summary>
public record CreateClipResponse
{
    /// <summary>
    /// A URL that you can use to edit the clip’s title, identify the part of the clip to publish, and publish the clip.
    /// The URL is valid for up to 24 hours or until the clip is published, whichever comes first.
    /// <see href="https://help.twitch.tv/s/article/how-to-use-clips">Learn More</see>.
    /// </summary>
    public required string EditUrl { get; init; }
    /// <summary>
    /// An id that uniquely identifies the clip.
    /// </summary>
    public required string Id { get; init; }
}
